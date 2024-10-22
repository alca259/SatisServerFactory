namespace StylableWinFormsControls.LayoutInternals;

/// Source: MeasureTextCache in System.Windows.Forms.Layout.LayoutUtils (part of it)
/// Cache mechanism added for VSWhidbey 500516
/// 3000 character strings take 9 seconds to load the form
internal class MeasureTextCache
{
    private Size _unconstrainedPreferredSize = INVALID_SIZE;

    private static readonly Size MAX_SIZE = new(int.MaxValue, int.MaxValue);
    private static readonly Size INVALID_SIZE = new(int.MinValue, int.MinValue);

    /// InvalidateCache
    /// Clears out the cached values, should be called whenever Text, Font or a TextFormatFlag has changed
    public void InvalidateCache()
    {
        _unconstrainedPreferredSize = INVALID_SIZE;
    }

    /// TextRequiresWordBreak
    /// If you give the text all the space in the world it wants, then there should be no reason
    /// for it to break on a word.  So we find out what the unconstrained size is (Int32.MaxValue, Int32.MaxValue)
    /// for a string - eg. 35, 13.  If the size passed in has a larger width than 35, then we know that
    /// the WordBreak flag is not necessary.
    public bool TextRequiresWordBreak(string text, Font font, Size size, TextFormatFlags flags)
    {
        // if the unconstrained size of the string is larger than the proposed width
        // we need the word break flag, otherwise we dont, its a perf hit to use it.
        return getUnconstrainedSize(text, font, flags).Width > size.Width;
    }

    /// GetUnconstrainedSize
    /// Gets the unconstrained (Int32.MaxValue, Int32.MaxValue) size for a piece of text
    private Size getUnconstrainedSize(string text, Font font, TextFormatFlags flags)
    {
        if (_unconstrainedPreferredSize == INVALID_SIZE)
        {
            // we also investigated setting the SingleLine flag, however this did not yield as much benefit as the word break
            // and had possibility of causing internationalization issues.

            flags &= ~TextFormatFlags.WordBreak; // rip out the wordbreak flag
            _unconstrainedPreferredSize = TextRenderer.MeasureText(text, font, MAX_SIZE, flags);
        }

        return _unconstrainedPreferredSize;
    }
}
