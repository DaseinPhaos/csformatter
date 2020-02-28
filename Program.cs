using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.CSharp.Formatting;

namespace csformatter {
    class Program {
        static void Main(string[] args) {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var begin = sw.Elapsed;
            var cliArgs = EntryPoint.Cli.Parse<CliArgs>(args);
            if(string.IsNullOrEmpty(cliArgs.DstFileName)) {
                cliArgs.DstFileName = cliArgs.SrcFileName;
            }

            var workspace = new AdhocWorkspace();
            var project = workspace.AddProject("Debug Project",LanguageNames.CSharp);
            var cliParsedTs = sw.Elapsed;
            var cliDuration = cliParsedTs - begin;
            SourceText sourceText;
            using(var fs = System.IO.File.OpenRead(cliArgs.SrcFileName)) {
                sourceText = SourceText.From(fs);
            }
            var readFileTs = sw.Elapsed;
            var readFileDuration = readFileTs - cliParsedTs;

            var doc = workspace.AddDocument(project.Id,cliArgs.SrcFileName,sourceText);
            var formatOptionSet = doc.GetOptionsAsync().Result
                .WithChangedOption(CSharpFormattingOptions.SpacingAfterMethodDeclarationName,cliArgs.SpacingAfterMethodDeclarationName)
                .WithChangedOption(CSharpFormattingOptions.IndentBlock,cliArgs.IndentBlock)
                .WithChangedOption(CSharpFormattingOptions.IndentSwitchSection,cliArgs.IndentSwitchSection)
                .WithChangedOption(CSharpFormattingOptions.IndentSwitchCaseSection,cliArgs.IndentSwitchCaseSection)
                .WithChangedOption(CSharpFormattingOptions.IndentSwitchCaseSectionWhenBlock,cliArgs.IndentSwitchCaseSectionWhenBlock)
                .WithChangedOption(CSharpFormattingOptions.WrappingPreserveSingleLine,cliArgs.WrappingPreserveSingleLine)
                .WithChangedOption(CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine,cliArgs.WrappingKeepStatementsOnSingleLine)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes,cliArgs.NewLinesForBracesInTypes)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInMethods,cliArgs.NewLinesForBracesInMethods)
                .WithChangedOption(CSharpFormattingOptions.IndentBraces,cliArgs.IndentBraces)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInProperties,cliArgs.NewLinesForBracesInProperties)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInAnonymousMethods,cliArgs.NewLinesForBracesInAnonymousMethods)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInControlBlocks,cliArgs.NewLinesForBracesInControlBlocks)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInAnonymousTypes,cliArgs.NewLinesForBracesInAnonymousTypes)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers,cliArgs.NewLinesForBracesInObjectCollectionArrayInitializers)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInLambdaExpressionBody,cliArgs.NewLinesForBracesInLambdaExpressionBody)
                .WithChangedOption(CSharpFormattingOptions.NewLineForElse,cliArgs.NewLineForElse)
                .WithChangedOption(CSharpFormattingOptions.NewLineForCatch,cliArgs.NewLineForCatch)
                .WithChangedOption(CSharpFormattingOptions.NewLineForFinally,cliArgs.NewLineForFinally)
                .WithChangedOption(CSharpFormattingOptions.NewLineForMembersInObjectInit,cliArgs.NewLineForMembersInObjectInit)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInAccessors,cliArgs.NewLinesForBracesInAccessors)
                .WithChangedOption(CSharpFormattingOptions.NewLineForMembersInAnonymousTypes,cliArgs.NewLineForMembersInAnonymousTypes)
                .WithChangedOption(CSharpFormattingOptions.SpaceBeforeDot,cliArgs.SpaceBeforeDot)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinMethodDeclarationParenthesis,cliArgs.SpaceWithinMethodDeclarationParenthesis)
                .WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptyMethodDeclarationParentheses,cliArgs.SpaceBetweenEmptyMethodDeclarationParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterMethodCallName,cliArgs.SpaceAfterMethodCallName)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinMethodCallParentheses,cliArgs.SpaceWithinMethodCallParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptyMethodCallParentheses,cliArgs.SpaceBetweenEmptyMethodCallParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterControlFlowStatementKeyword,cliArgs.SpaceAfterControlFlowStatementKeyword)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinExpressionParentheses,cliArgs.SpaceWithinExpressionParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinCastParentheses,cliArgs.SpaceWithinCastParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinOtherParentheses,cliArgs.SpaceWithinOtherParentheses)
                .WithChangedOption(CSharpFormattingOptions.SpaceBeforeSemicolonsInForStatement,cliArgs.SpaceBeforeSemicolonsInForStatement)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterCast,cliArgs.SpaceAfterCast)
                .WithChangedOption(CSharpFormattingOptions.SpaceBeforeOpenSquareBracket,cliArgs.SpaceBeforeOpenSquareBracket)
                .WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptySquareBrackets,cliArgs.SpaceBetweenEmptySquareBrackets)
                .WithChangedOption(CSharpFormattingOptions.SpaceWithinSquareBrackets,cliArgs.SpaceWithinSquareBrackets)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterColonInBaseTypeDeclaration,cliArgs.SpaceAfterColonInBaseTypeDeclaration)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterComma,cliArgs.SpaceAfterComma)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterDot,cliArgs.SpaceAfterDot)
                .WithChangedOption(CSharpFormattingOptions.SpaceAfterSemicolonsInForStatement,cliArgs.SpaceAfterSemicolonsInForStatement)
                .WithChangedOption(CSharpFormattingOptions.SpaceBeforeColonInBaseTypeDeclaration,cliArgs.SpaceBeforeColonInBaseTypeDeclaration)
                .WithChangedOption(CSharpFormattingOptions.SpaceBeforeComma,cliArgs.SpaceBeforeComma)
                .WithChangedOption(CSharpFormattingOptions.SpacesIgnoreAroundVariableDeclaration,cliArgs.SpacesIgnoreAroundVariableDeclaration)
                .WithChangedOption(CSharpFormattingOptions.NewLineForClausesInQuery,cliArgs.NewLineForClausesInQuery)
                .WithChangedOption(CSharpFormattingOptions.SpacingAroundBinaryOperator,cliArgs.SpacingAroundBinaryOperator);

            var docTs = sw.Elapsed;
            var docDuration = docTs - readFileTs;

            var formatted = Microsoft.CodeAnalysis.Formatting.Formatter.FormatAsync(
                doc,
                formatOptionSet
            ).Result;
            var formattedSrc = formatted.GetTextAsync().Result;
            var formatTs = sw.Elapsed;
            var formatDuration = formatTs - docTs;

            using(var os = System.IO.File.Create(cliArgs.DstFileName,4096,System.IO.FileOptions.WriteThrough))
            using(var tw = new System.IO.StreamWriter(os)) {
                formattedSrc.Write(tw);
            }
            var fileWriteTs = sw.Elapsed;
            var fileWriteDuration = fileWriteTs - formatTs;
            System.Console.WriteLine($"cli: {cliDuration}, read: {readFileDuration}, doc: {docDuration},format: {formatDuration}, write: {fileWriteDuration}");
        }

        class CliArgs:EntryPoint.BaseCliArguments {
            public CliArgs() : base("") { }

            public override void OnUserFacingException(EntryPoint.Exceptions.UserFacingException e,string message) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid argument: " + message);
                Console.ResetColor();
                Environment.Exit(1);
            }

            [EntryPoint.Required]
            [EntryPoint.Operand(Position: 1)]
            [EntryPoint.Help("Src File Name")]
            public string SrcFileName {
                get; set;
            }

            [EntryPoint.Operand(Position: 2)]
            [EntryPoint.Help("Dst File Name")]
            public string DstFileName {
                get; set;
            }

            [EntryPoint.Option("SpacingAfterMethodDeclarationName")]
            public bool SpacingAfterMethodDeclarationName {
                get; set;
            }
            [EntryPoint.Option("IndentBlock")]
            public bool IndentBlock {
                get; set;
            }
            [EntryPoint.Option("IndentSwitchSection")]
            public bool IndentSwitchSection {
                get; set;
            }
            [EntryPoint.Option("IndentSwitchCaseSection")]
            public bool IndentSwitchCaseSection {
                get; set;
            }
            [EntryPoint.Option("IndentSwitchCaseSectionWhenBlock")]
            public bool IndentSwitchCaseSectionWhenBlock {
                get; set;
            }
            // TODO: public static Option<LabelPositionOptions> LabelPositioning { get; }
            [EntryPoint.Option("WrappingPreserveSingleLine")]
            public bool WrappingPreserveSingleLine {
                get; set;
            }
            [EntryPoint.Option("WrappingKeepStatementsOnSingleLine")]
            public bool WrappingKeepStatementsOnSingleLine {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInTypes")]
            public bool NewLinesForBracesInTypes {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInMethods")]
            public bool NewLinesForBracesInMethods {
                get; set;
            }
            [EntryPoint.Option("IndentBraces")]
            public bool IndentBraces {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInProperties")]
            public bool NewLinesForBracesInProperties {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInAnonymousMethods")]
            public bool NewLinesForBracesInAnonymousMethods {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInControlBlocks")]
            public bool NewLinesForBracesInControlBlocks {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInAnonymousTypes")]
            public bool NewLinesForBracesInAnonymousTypes {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInObjectCollectionArrayInitializers")]
            public bool NewLinesForBracesInObjectCollectionArrayInitializers {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInLambdaExpressionBody")]
            public bool NewLinesForBracesInLambdaExpressionBody {
                get; set;
            }
            [EntryPoint.Option("NewLineForElse")]
            public bool NewLineForElse {
                get; set;
            }
            [EntryPoint.Option("NewLineForCatch")]
            public bool NewLineForCatch {
                get; set;
            }
            [EntryPoint.Option("NewLineForFinally")]
            public bool NewLineForFinally {
                get; set;
            }
            [EntryPoint.Option("NewLineForMembersInObjectInit")]
            public bool NewLineForMembersInObjectInit {
                get; set;
            }
            [EntryPoint.Option("NewLinesForBracesInAccessors")]
            public bool NewLinesForBracesInAccessors {
                get; set;
            }
            [EntryPoint.Option("NewLineForMembersInAnonymousTypes")]
            public bool NewLineForMembersInAnonymousTypes {
                get; set;
            }
            [EntryPoint.Option("SpaceBeforeDot")]
            public bool SpaceBeforeDot {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinMethodDeclarationParenthesis")]
            public bool SpaceWithinMethodDeclarationParenthesis {
                get; set;
            }
            [EntryPoint.Option("SpaceBetweenEmptyMethodDeclarationParentheses")]
            public bool SpaceBetweenEmptyMethodDeclarationParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterMethodCallName")]
            public bool SpaceAfterMethodCallName {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinMethodCallParentheses")]
            public bool SpaceWithinMethodCallParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceBetweenEmptyMethodCallParentheses")]
            public bool SpaceBetweenEmptyMethodCallParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterControlFlowStatementKeyword")]
            public bool SpaceAfterControlFlowStatementKeyword {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinExpressionParentheses")]
            public bool SpaceWithinExpressionParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinCastParentheses")]
            public bool SpaceWithinCastParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinOtherParentheses")]
            public bool SpaceWithinOtherParentheses {
                get; set;
            }
            [EntryPoint.Option("SpaceBeforeSemicolonsInForStatement")]
            public bool SpaceBeforeSemicolonsInForStatement {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterCast")]
            public bool SpaceAfterCast {
                get; set;
            }
            [EntryPoint.Option("SpaceBeforeOpenSquareBracket")]
            public bool SpaceBeforeOpenSquareBracket {
                get; set;
            }
            [EntryPoint.Option("SpaceBetweenEmptySquareBrackets")]
            public bool SpaceBetweenEmptySquareBrackets {
                get; set;
            }
            [EntryPoint.Option("SpaceWithinSquareBrackets")]
            public bool SpaceWithinSquareBrackets {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterColonInBaseTypeDeclaration")]
            public bool SpaceAfterColonInBaseTypeDeclaration {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterComma")]
            public bool SpaceAfterComma {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterDot")]
            public bool SpaceAfterDot {
                get; set;
            }
            [EntryPoint.Option("SpaceAfterSemicolonsInForStatement")]
            public bool SpaceAfterSemicolonsInForStatement {
                get; set;
            }
            [EntryPoint.Option("SpaceBeforeColonInBaseTypeDeclaration")]
            public bool SpaceBeforeColonInBaseTypeDeclaration {
                get; set;
            }
            [EntryPoint.Option("SpaceBeforeComma")]
            public bool SpaceBeforeComma {
                get; set;
            }
            [EntryPoint.Option("SpacesIgnoreAroundVariableDeclaration")]
            public bool SpacesIgnoreAroundVariableDeclaration {
                get; set;
            }
            [EntryPoint.Option("NewLineForClausesInQuery")]
            public bool NewLineForClausesInQuery {
                get; set;
            }

            [EntryPoint.OptionParameter("SpacingAroundBinaryOperator",'b')]
            public BinaryOperatorSpacingOptions SpacingAroundBinaryOperator {
                get; set;
            }
        }
    }
}
