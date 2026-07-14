namespace Weather.Controls.SearchControl;

public partial class SearchViewModel(IMessenger messenger, IEnumerable<string> list, string baseSearch) : ObservableObject
{
    private readonly IMessenger _Messenger = messenger;
    private readonly IEnumerable<string> _List = list;

    [ObservableProperty]
    public partial string SearchText { get; set; } = baseSearch;

    [ObservableProperty]
    public partial ObservableCollection<string> Suggestions { get; set; } = [];

    [ObservableProperty]
    public partial string? SelectedSuggestion { get; set; }

    [ObservableProperty]
    public partial bool IsSuggestionsOpen { get; set; }

    [RelayCommand]
    private void SelectSuggestion(string suggestion)
    {
        if (suggestion != null)
        {
            SearchText = suggestion;
            IsSuggestionsOpen = false;
            Suggestions.Clear();
        }
    }

    partial void OnSearchTextChanged(string value) => UpdateSuggestions(value);

    private void UpdateSuggestions(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            Suggestions.Clear();
            IsSuggestionsOpen = false;
            return;
        }
        var filtered = _List
            .Where(item => item.Contains(query, System.StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToList();
        Suggestions.Clear();
        foreach (var item in filtered)
            Suggestions.Add(item);
        IsSuggestionsOpen = Suggestions.Any();
    }

    [RelayCommand]
    private void NewSelectedItem()
    {
        if (string.IsNullOrEmpty(SearchText) || !_List.Any(x => x.Equals(SearchText, StringComparison.OrdinalIgnoreCase)))
        {
            SearchText = "";
            return;
        }
        _Messenger.Send(new NewSearchValueMessage(SearchText));
    }
}