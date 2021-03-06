﻿using System;
using CommandLine;
using CommandLine.Text;

namespace gdbconfig
{
    // Class to receive parsed values
    public class Options
    {
        #region Input
        
        [ValueOption(0)]
        [Option('i', "input", Required = false, HelpText = "Input .sde file for connection")]
        public string InputSDEFile { get; set; }

        [ValueOption(1)]
        [Option("p1", Required = false)]
        public string Parameter1 { get; set; }

        [ValueOption(2)]
        [Option("p2", Required = false)]
        public string Parameter2 { get; set; }

        [ValueOption(3)]
        [Option("p3", Required = false)]
        public string Parameter3 { get; set; }

        #endregion

        #region Commands

        [Option("add-domain", DefaultValue = false, HelpText = "Add a Domain value", MutuallyExclusiveSet = "command")]
        public bool AddDomain { get; set; }

        [Option("order-domain", DefaultValue = false, HelpText = "Order a Domain", MutuallyExclusiveSet = "command")]
        public bool OrderDomain { get; set; }

        [Option("list-domain", DefaultValue = false, HelpText = "List a Domain Contents", MutuallyExclusiveSet = "command")]
        public bool ListDomain { get; set; }

        [Option("remove-domain", DefaultValue = false, HelpText = "Remove a Domain value", MutuallyExclusiveSet = "command")]
        public bool RemoveDomain { get; set; }

        [Option("add-class-model-name", DefaultValue = false, HelpText = "Add a Class Model Name", MutuallyExclusiveSet = "command")]
        public bool AddClassModelName { get; set; }

        [Option("list-class-model-name", DefaultValue = false, HelpText = "List Class Model Names", MutuallyExclusiveSet = "command")]
        public bool ListClassModelNames { get; set; }

        [Option("remove-class-model-name", DefaultValue = false, HelpText = "Remove a Class Model Name", MutuallyExclusiveSet = "command")]
        public bool RemoveClassModelName { get; set; }

        [Option("add-field-model-name", DefaultValue = false, HelpText = "Add a Field Model Name", MutuallyExclusiveSet = "command")]
        public bool AddFieldModelName { get; set; }

        [Option("list-field-model-name", DefaultValue = false, HelpText = "List Field Model Names", MutuallyExclusiveSet = "command")]
        public bool ListFieldModelNames { get; set; }

        [Option("remove-field-model-name", DefaultValue = false, HelpText = "Remove a Field Model Name", MutuallyExclusiveSet = "command")]
        public bool RemoveFieldModelName { get; set; }

        #endregion
        
        #region Modifiers

        [Option('v', "version", DefaultValue = false, HelpText = "Display Version and Exit")]
        public bool Version { get; set; }

        [Option('h', "help", DefaultValue = false, HelpText = "Show Help and Usage")]
        public bool Help { get; set; }

        [Option('t', "test", DefaultValue = false, HelpText = "Test Connection and Exit", MutuallyExclusiveSet = "command")]
        public bool Test { get; set; }

        [Option('V', "verbose", DefaultValue = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('d', "dry-run", DefaultValue = false, HelpText = "Connects and validates, but does not perform any modifications.")]
        public bool DryRun { get; set; }

        [Option('p', "pause", DefaultValue = false, HelpText = "Pause for a key press before terminating.")]
        public bool Pause { get; set; }

        [Option('l', "list", DefaultValue = false, HelpText = "List resulting values after modification.")]
        public bool List { get; set; }

        [Option('n', "newline", DefaultValue = false, HelpText = "Do not output the trailing newline.")]
        public bool Newline { get; set; }

        #endregion

        #region Calculated

        public bool EsriLicenseRequired
        {
            get { return Test || AddDomain || OrderDomain || ListDomain || OrderDomain || RemoveDomain || AddClassModelName || AddFieldModelName || ListClassModelNames || ListFieldModelNames || RemoveClassModelName || RemoveFieldModelName; }
        }

        public bool ArcFMLicenseRequired
        {
            get { return AddClassModelName || AddFieldModelName || ListClassModelNames || ListFieldModelNames || RemoveClassModelName || RemoveFieldModelName; }
        }

        #endregion

        #region Help Generation

        [ParserState]
        public IParserState LastParserState { get; set; }

        //[HelpOption]
        public string GetUsage()
        {
            //return HelpText.AutoBuild(this, (current) => { });
            string helpText = HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            helpText += "  Example: gdbconfig Sample.sde -add-domain \"Domain Name\" \"Value\" \"Name\" \r\n";
            return helpText;
        }

        #endregion

        #region Parsers

        public static Options Default
        {
            get
            {
                Options defaults = new Options();
                var settings = new ParserSettings(true, true);
                var parser = new Parser(with => with.CaseSensitive = true);
                return defaults;
            }
        }

        #endregion
    }
}
