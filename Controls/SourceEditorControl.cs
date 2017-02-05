using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DodoIDE.Interfaces;
using ScintillaNET;
using System.IO;
using AutocompleteMenuNS;

namespace DodoIDE.Controls
{
    public partial class SourceEditorControl : UserControl
    {
        public string CurrentSelection
        {
            get;set;
        }

        public SourceEditorControl()
        {
            InitializeComponent();

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            SourceEditorBox.StyleResetDefault();
            SourceEditorBox.Styles[Style.Default].Font = "Consolas";
            SourceEditorBox.Styles[Style.Default].Size = 10;
            SourceEditorBox.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            SourceEditorBox.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            SourceEditorBox.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            SourceEditorBox.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            SourceEditorBox.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            SourceEditorBox.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            SourceEditorBox.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            SourceEditorBox.Styles[Style.Cpp.Word].Bold = true;
            SourceEditorBox.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            SourceEditorBox.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            SourceEditorBox.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            SourceEditorBox.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            SourceEditorBox.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            SourceEditorBox.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            SourceEditorBox.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            SourceEditorBox.Lexer = Lexer.Cpp;

            // Set the keywords
            SourceEditorBox.SetKeywords(0, "Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield" + 
                " Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield".ToLower() + 
                " Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield".ToUpper());
            SourceEditorBox.SetKeywords(1, "Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield" +
                " Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield".ToLower() +
                " Array Begin Break Byte Call Case Clone Const Continue Debug Declare Default Dup Elif Else Elseif Elsif End Float For Frame From Function Global Goto If Import Include Int Jmp Local Loop Offset OnExit Pointer Private Process Program Public Repeat Return Short Sizeof Step String Struct Switch To Type Until Varspace Void While Word Yield".ToUpper());
            /*
            SourceEditorBox.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            SourceEditorBox.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
            */

            SourceEditorBox.Margins[0].Width = 40;
            SourceEditorBox.Styles[Style.LineNumber].Font = "Consolas";
            SourceEditorBox.Margins[0].Type = MarginType.Number;

            autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(SourceEditorBox);

            // agregar autocompletado
        }

        public override string Text
        {
            get { return SourceEditorBox.Text; }
            set { SourceEditorBox.Text = value; }
        }

        public SourceCodeController ParentController { get; internal set; }

        private void SourceEditorBox_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public void SetMeOnTop() { ParentController.BringToFront(); }

        public event EventHandler SelectControl;
    }
}
