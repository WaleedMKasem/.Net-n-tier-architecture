using System;

namespace Arabia.Core.Enums
{
    public enum MessageType
    {
        Success,
        Error,
        Information,
        Warning
    }
    public enum Language
    {
        Arabic = 0,
        English = 1,
        Both = 2
    }
  
    public enum Controls
    {
        TextBox = 1,
        TextArea = 2,
        Numeric = 3,
        RadioButtonList = 4,
        CheckBox = 5,
        CheckBoxList = 6,
        Date = 7,
        DateTime = 8,
        DropDownList = 9
       // FileUpload = 6
    }
  

    public enum Validations
    {
        DefaultValue = 1,
        ExpectedValue = 2,
        MinWordCount = 3,
        MaxWordCount = 4,
        MinCharCount = 5,
        MaxCharCount = 6,
        MinLength = 7,
        MaxLength = 8,
        DateFormat = 9,
        DateFuturistic = 10,
        Collections = 11,
        SelectedValue = 12
    }
    public enum DateFormat
    {
        Gregorian = 1,
        Hijra = 2
    }
}
