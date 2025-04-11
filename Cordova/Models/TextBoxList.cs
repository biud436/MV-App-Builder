using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cordova.Models
{
    public class TextBoxList : List<Control>
    {

        public ListBox _plugins;

        public TextBox folderName
        {
            get { return this[0] as TextBox; }
        }

        public TextBox keyPath
        {
            get { return this[1] as TextBox; }
        }

        public TextBox gameName
        {
            get { return this[2] as TextBox; }
        }

        public TextBox keyAlias
        {
            get { return this[3] as TextBox; }
        }

        public TextBox passWord
        {
            get { return this[4] as TextBox; }
        }

        public TextBox packageName
        {
            get { return this[5] as TextBox; }
        }

        public TextBox keyOU
        {
            get { return this[6] as TextBox; }
        }

        public TextBox keyO
        {
            get { return this[7] as TextBox; }
        }

        public TextBox keyL
        {
            get { return this[8] as TextBox; }
        }

        public TextBox keyS
        {
            get { return this[9] as TextBox; }
        }

        public TextBox keyC
        {
            get { return this[10] as TextBox; }
        }

        public ComboBox orientation
        {
            get { return this[11] as ComboBox;  }
        }

        public ComboBox fullscreen
        {
            get { return this[12] as ComboBox;  }
        }

        public ComboBox minSdkVersion
        {
            get { return this[13] as ComboBox;  }
        }

        public ComboBox targetSdkVersion
        {
            get { return this[14] as ComboBox; }
        }

        public TextBox settingGameFolder
        {
            get { return this[15] as TextBox; }
        }

        public ComboBox biuldMode
        {
            get { return this[16] as ComboBox;  }
        }

        public ComboBox compileSdkVersion
        {
            get { return this[17] as ComboBox; }
        }

        public ListBox plugins
        {
            get { return _plugins; }
            set { _plugins = value; }
        }

    }
}
