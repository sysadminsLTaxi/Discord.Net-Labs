using Newtonsoft.Json;
using System.Linq;

namespace Discord.API
{
    internal class ApplicationCommandOption
    {
        [JsonProperty("type")]
        public ApplicationCommandOptionType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("default")]
        public Optional<bool> Default { get; set; }

        [JsonProperty("required")]
        public Optional<bool> Required { get; set; }

        [JsonProperty("choices")]
        public Optional<ApplicationCommandOptionChoice[]> Choices { get; set; }

        [JsonProperty("options")]
        public Optional<ApplicationCommandOption[]> Options { get; set; }

        [JsonProperty("autocomplete")]
        public Optional<bool> Autocomplete { get; set; }

        [JsonProperty("channel_types")]
        public Optional<ChannelType[]> ChannelTypes { get; set; }

        public ApplicationCommandOption() { }

        public ApplicationCommandOption(IApplicationCommandOption cmd)
        {
            Choices = cmd.Choices.Select(x => new ApplicationCommandOptionChoice
            {
                Name = x.Name,
                Value = x.Value
            }).ToArray();

            Options = cmd.Options.Select(x => new ApplicationCommandOption(x)).ToArray();

            ChannelTypes = cmd.ChannelTypes.ToArray();

            Required = cmd.IsRequired ?? Optional<bool>.Unspecified;
            Default = cmd.IsDefault ?? Optional<bool>.Unspecified;

            Name = cmd.Name;
            Type = cmd.Type;
            Description = cmd.Description;
        }
        public ApplicationCommandOption(ApplicationCommandOptionProperties option)
        {
            Choices = option.Choices?.Select(x => new ApplicationCommandOptionChoice
            {
                Name = x.Name,
                Value = x.Value
            }).ToArray() ?? Optional<ApplicationCommandOptionChoice[]>.Unspecified;

            Options = option.Options?.Select(x => new ApplicationCommandOption(x)).ToArray() ?? Optional<ApplicationCommandOption[]>.Unspecified;

            Required = option.Required ?? Optional<bool>.Unspecified;

            Default = option.Default ?? Optional<bool>.Unspecified;

            ChannelTypes = option.ChannelTypes?.ToArray() ?? Optional<ChannelType[]>.Unspecified;

            Name = option.Name;
            Type = option.Type;
            Description = option.Description;
            Autocomplete = option.Autocomplete;
        }
    }
}
