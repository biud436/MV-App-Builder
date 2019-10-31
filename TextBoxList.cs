using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cordova_Builder
{
    public class TextBoxList : List<TextBox>
    {

        public enum Type
        {
            FOLDER_NAME = 0,
            KEY_PATH,
            GAME_NAME,
            KEY_ALIAS,
            PASSWORD,
            PACKAGE_NAME,
            KEY_OU,
            KEY_O,
            KEY_L,
            KEY_S,
            KEY_C,
        };

        public TextBox folderName
        {
            get { return this[0]; }
        }

        public TextBox keyPath
        {
            get { return this[1]; }
        }

        public TextBox gameName
        {
            get { return this[2]; }
        }

        public TextBox keyAlias
        {
            get { return this[3]; }
        }

        public TextBox passWord
        {
            get { return this[4]; }
        }

        public TextBox packageName
        {
            get { return this[5]; }
        }

        public TextBox keyOU
        {
            get { return this[6]; }
        }

        public TextBox keyO
        {
            get { return this[7]; }
        }

        public TextBox keyL
        {
            get { return this[8]; }
        }

        public TextBox keyS
        {
            get { return this[9]; }
        }

        public TextBox keyC
        {
            get { return this[10]; }
        }

    }
}
