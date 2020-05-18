/*
TSGui, a Graphical User Interface that replaces the TShock Console
Copyright (C) 2015 Ancientgods & magnusi
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/


using System;
using System.IO;
using System.Threading;

namespace TSGui.Extensions
{
    public class TaskReader : TextReader
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private TextReader _reader;
        private string _text;
        private Action<string> _action;

        public void SendText(string text)
        {
            _text = text;
            _resetEvent.Set();

            if (_action != null)
                _action(_text);
        }

        public TaskReader(TextReader textReader)
        {
            _reader = textReader;
        }

        public TaskReader(TextReader textReader, Action<string> action) : this(textReader)
        {
            _action = action;
        }

        public override string ReadLine()
        {
            _resetEvent.WaitOne();
            return _text;
        }
    }
}
