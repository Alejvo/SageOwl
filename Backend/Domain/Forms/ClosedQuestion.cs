using Domain.Forms.Dtos;

namespace Domain.Forms;

public class ClosedQuestion : Question
{
    private readonly List<Option> _options;

    public IReadOnlyCollection<Option> Options => [.. _options];

    public ClosedQuestion() { }
    public ClosedQuestion(string text) : base(text)
    {
    }

    public void AddOption(Option option)
    {
        option.SetQuestion(this);
        _options.Add(option);
    }

    public void SyncOptions(IEnumerable<OptionInput> incoming)
    {
        var existing = _options
       .Where(o => o.Id != Guid.Empty)
       .ToDictionary(o => o.Id);

        var incomingIds = incoming
            .Where(o => o.Id != Guid.Empty)
            .Select(o => o.Id)
            .ToHashSet();

        var toRemove = _options
            .Where(o => o.Id != Guid.Empty && !incomingIds.Contains(o.Id))
            .ToList();

        foreach (var option in toRemove)
        {
            _options.Remove(option);
        }

        foreach (var op in incoming)
        {
            if (op.Id != Guid.Empty && existing.TryGetValue(op.Id.Value, out var existingOption))
            {
                // UPDATE
                existingOption.Update(op.Value, op.IsCorrect);
            }
            else
            {
                // CREATE
                var newOption = Option.Create(op.Value, op.IsCorrect);
                newOption.SetQuestion(this);
                _options.Add(newOption);
            }
        }

    }
}
