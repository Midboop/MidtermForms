using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
    /// <summary>
    /// Bindable page.
    /// </summary>
    public interface IBindablePage
    {
        /// <summary>
        /// Gets or sets the binding context.
        /// </summary>
        object BindingContext { get; set; }
    }
}
