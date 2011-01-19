#region Using definitions
//  sort alphabetically
//  group like references
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// <    commented line break - group like references
using Microsoft.Runtime;    // first external reference
// <    commented line break
using Engine_01.Interfaces; // second external reference
#endregion

namespace Engine_01
{
    /// <summary>
    /// ClassTemplate
    /// 
    ///     This is a class template. Please use this template for your
    ///     classes. In most circumstances, all using clauses should be 
    ///     defined at the top in alphabetical order. Separate external 
    ///     references as I have shown commented above.
    ///     
    ///     All public methods should be commented heavily. If something 
    ///     is not clear, ask questions in a remarks tag under the comments.
    ///     I don't know all the markup that can be used in the Xml 
    ///     descriptions so some help with those will be useful for 
    ///     documentation.
    ///     
    ///     Make comments throughout methods where something may not be 
    ///     clear. Again, make comments to ask questions where something
    ///     might not be clear.
    /// </summary>
    public class ClassTemplate
    {
        //  While a class is being developed, the regions do not have to 
        //  be removed. When we finalize a class, we should remove regions
        //  that are unused.

        #region Fields
        //  Include a fields section only in objects where fields are
        //  defined.
        #endregion

        #region Init
        //  Include an init section only in objects where a constructor
        //  is defined.
        #endregion
 
        #region Functions
        //  Include a functions section only in objects where functions
        //  are defined.
        #endregion

        #region Properties
        //  Include a properies section only in objects where properties
        //  are defined.
        #endregion
   }
}
