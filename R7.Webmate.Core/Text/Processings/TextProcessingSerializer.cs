using R7.Webmate.Core.Text.Commands;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace R7.Webmate.Core.Text.Processings
{
    public class TextProcessingSerializer
    {
        public string Serialize (ITextProcessing textProcessing)
        {
            var serializer = new SerializerBuilder ()
                    .WithNamingConvention (HyphenatedNamingConvention.Instance)
                    //.EnsureRoundtrip ()
                    .WithTagMapping ("tag:yaml.org,2002:if-option", typeof (IfOptionCommand))
                    .WithTagMapping ("tag:yaml.org,2002:regex-replace", typeof (RegexReplaceCommand))
                    .WithTagMapping ("tag:yaml.org,2002:regex-to-lower", typeof (RegexToLowerCommand))
                    .WithTagMapping ("tag:yaml.org,2002:replace-entities-with-chars", typeof (ReplaceEntitiesWithCharsCommand))
                    .WithTagMapping ("tag:yaml.org,2002:replace", typeof (ReplaceCommand))
                    .WithTagMapping ("tag:yaml.org,2002:trim", typeof (TrimCommand))
                    .WithTagMapping ("tag:yaml.org,2002:append", typeof (AppendCommand))
                    .WithTagMapping ("tag:yaml.org,2002:prepend", typeof (PrependCommand))
                    .WithTagMapping ("tag:yaml.org,2002:exit", typeof (ExitCommand))
                    .Build ();

            return serializer.Serialize (textProcessing);
        }

        public ITextProcessing Deserialize (string yaml)
        {
            var deserializer = new DeserializerBuilder ()
                    .WithNamingConvention (HyphenatedNamingConvention.Instance)
                    .WithTagMapping ("tag:yaml.org,2002:if-option", typeof (IfOptionCommand))
                    .WithTagMapping ("tag:yaml.org,2002:regex-replace", typeof (RegexReplaceCommand))
                    .WithTagMapping ("tag:yaml.org,2002:regex-to-lower", typeof (RegexToLowerCommand))
                    .WithTagMapping ("tag:yaml.org,2002:replace-entities-with-chars", typeof (ReplaceEntitiesWithCharsCommand))
                    .WithTagMapping ("tag:yaml.org,2002:replace", typeof (ReplaceCommand))
                    .WithTagMapping ("tag:yaml.org,2002:trim", typeof (TrimCommand))
                    .WithTagMapping ("tag:yaml.org,2002:append", typeof (AppendCommand))
                    .WithTagMapping ("tag:yaml.org,2002:prepend", typeof (PrependCommand))
                    .WithTagMapping ("tag:yaml.org,2002:exit", typeof (ExitCommand))
                    .Build ();

            return deserializer.Deserialize<TextProcessing> (yaml);
        }
    }
}
