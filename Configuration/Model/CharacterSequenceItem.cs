﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sequencer.Configuration.Model
{
    [XmlRoot(ElementName = "Characters")]
    public class CharacterSequenceItem : SequenceItem
    {
        public CharacterSequenceItem()
        {
            Length = 5;
            LengthStrength = StrengthEnum.Full;
            AllowDuplicate = true;
            Characters = new OverridingCharacterList();
        }

        public OverridingCharacterList Characters { get; set; }

        public override double entropy(PasswordSequenceConfiguration config)
        {
            double entropyVal = 0;

            if (Length > 0)
            {
                if (Characters.Count > 0)
                {
                    entropyVal += Math.Log(Characters.Count, 2);
                }
                if (config.DefaultCharacters.Count > 0 && !Characters.Override)
                {
                    entropyVal += Math.Log(config.DefaultCharacters.Count, 2);
                }
                entropyVal *= Length;
            }
            /* TODO: other properties */

            return entropyVal;
        }

        [XmlAttribute("allowDuplicate")]
        public bool AllowDuplicate { get; set; }

        [XmlAttribute("length")]
        public uint Length { get; set; }

        [XmlIgnore]
        private StrengthEnum _myLenStren;
        public StrengthEnum LengthStrength
        {
            get { return _myLenStren; }
            set
            {
                if (value > StrengthEnum.Full)
                    _myLenStren = StrengthEnum.Full;
                else if (value < 0)
                    _myLenStren = 0;
                else
                    _myLenStren = value;
            }
        }

        [XmlAttribute("lengthStrength")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string XmlLengthStrength
        {
            get { return LengthStrength.ToString().ToLower(); }
            set { LengthStrength = (StrengthEnum)Enum.Parse(typeof(StrengthEnum), value, true); }
        }
    }
}
