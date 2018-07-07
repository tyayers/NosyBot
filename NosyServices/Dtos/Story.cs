using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace NosyServices.Dtos
{
    [TableName("Users")]
    [PrimaryKey("UserId")]
    public class StoryRecord : NosyCommon.Dtos.Story 
    {
    }
}
