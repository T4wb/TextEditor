﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextEditor
{
    public class DocumentManager
    {
        private string _currentFile;
        private RichTextBox _textBox;

        public DocumentManager(RichTextBox textBox)
        {
            _textBox = textBox;
        }

        public bool OpenDocument()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Rich Text Document | *.rtf | Text Document | *.txt"
            };
            
            if (dlg.ShowDialog() == true)
            {
                _currentFile = dlg.FileName;

                using (Stream stream = dlg.OpenFile())
                {
                    TextRange range = new TextRange(
                        _textBox.Document.ContentStart,
                        _textBox.Document.ContentEnd
                        );

                    range.Load(stream, DataFormats.Rtf);
                }

                return true;
            }

            return false;
        }

        public bool SaveDocument()
        {
            if (string.IsNullOrEmpty(_currentFile)) return SaveDocumentAs();
            else
            {
                using (Stream stream =
                    new FileStream(_currentFile, FileMode.Create))
                {
                    TextRange range = new TextRange(
                        _textBox.Document.ContentStart,
                        _textBox.Document.ContentEnd
                        );

                    range.Save(stream, DataFormats.Rtf);
                }

                return true;
            }
        }
        
        public bool SaveDocumentAs()
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Rich Text Document | *.rtf | Text Document | *.txt"
            };

            if (dlg.ShowDialog() == true)
            {
                _currentFile = dlg.FileName;

                return SaveDocument();
            }

            return false;
        }

        /// <summary>
        /// This method format the currently selected text.
        /// </summary>
        /// <param name="property">Dependency property</param>
        /// <param name="value">Value for setting the property</param>
        public void ApplyToSelection(DependencyProperty property, object value)
        {
            if (value != null)
            {
                _textBox.Selection.ApplyPropertyValue(property, value);
            }
        }

        public void NewDocument()
        {
            _currentFile = null;
            _textBox.Document = new FlowDocument();
        }

        public bool CanSaveDocument()
        {
            return !string.IsNullOrEmpty(_currentFile);
        }
    }
}
