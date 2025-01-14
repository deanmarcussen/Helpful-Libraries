using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    public static class ResultExecutingContextExtensions
    {
        /// <summary>
        /// Indicates if the current result is a full view rendering result (i.e. where you can properly inject shapes
        /// into the Layout) or if the <see cref="AdminAttribute"/> is applied.
        /// </summary>
        public static bool IsNotFullViewRenderingOrIsAdmin(this ResultExecutingContext context) =>
            context.IsNotFullViewRendering() || context.IsAdmin();

        /// <summary>
        /// Indicates if the current result is a full view rendering result (i.e. where you can properly inject shapes
        /// into the Layout) or if the <see cref="AdminAttribute"/> is NOT applied.
        /// </summary>
        public static bool IsNotFullViewRenderingOrIsNotAdmin(this ResultExecutingContext context) =>
            context.IsNotFullViewRendering() || !context.IsAdmin();

        /// <summary>
        /// Indicates if the <see cref="AdminAttribute"/> is applied to the current result.
        /// </summary>
        public static bool IsAdmin(this ResultExecutingContext context) =>
            AdminAttribute.IsApplied(context.HttpContext);

        /// <summary>
        /// Indicates if the current result is a full view rendering result (i.e. where you can properly inject shapes
        /// into the Layout).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The URL /Admin/Media/MediaApplication from OrchardCore.Media will be a full view rendering though.
        /// </para>
        /// </remarks>
        public static bool IsNotFullViewRendering(this ResultExecutingContext context) =>
            context.Result is not ViewResult and not PageResult;
    }
}
