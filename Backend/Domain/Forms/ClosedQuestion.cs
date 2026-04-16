using Domain.Forms.Dtos;

namespace Domain.Forms;

public class ClosedQuestion : Question
{
    private readonly List<Option> _options = [];

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

        _options.RemoveAll(x => !incomingIds.Contains(x.Id));

        foreach (var op in incoming)
        {
            if (op.Id is Guid id && existing.TryGetValue(id, out var existingOption))
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
