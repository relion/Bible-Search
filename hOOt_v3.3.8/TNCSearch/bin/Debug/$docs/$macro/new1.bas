Attribute VB_Name = "NewMacros"
Sub ClearMark()
'
' ClearMark Macro
' Macro recorded ?08/12/2006 by Aryeh Tuchfeld
'
    If False Then 'Cassics:
        With Selection.Font
            With .Shading
                .Texture = wdTextureNone
                .ForegroundPatternColor = wdColorYellow
                .BackgroundPatternColor = wdColorAutomatic
            End With
            .Borders(1).LineStyle = wdLineStyleNone
            .Borders.Shadow = False
        End With
        With Options
            .DefaultBorderLineStyle = wdLineStyleSingle
            .DefaultBorderLineWidth = wdLineWidth050pt
            .DefaultBorderColor = wdColorAutomatic
        End With
    Else 'Improved for some situations...
            With Selection.Font
            With .Shading
                .Texture = wdTextureSolid
                .ForegroundPatternColor = wdColorTurquoise
                .BackgroundPatternColor = wdColorAutomatic
            End With
            .Borders(1).LineStyle = wdLineStyleNone
            .Borders.Shadow = False
        End With
        With Options
            .DefaultBorderLineStyle = wdLineStyleSingle
            .DefaultBorderLineWidth = wdLineWidth050pt
            .DefaultBorderColor = wdColorAutomatic
        End With
        With Selection.Font
            With .Shading
                .Texture = wdTextureNone
                .ForegroundPatternColor = wdColorAutomatic
                .BackgroundPatternColor = wdColorAutomatic
            End With
            .Borders(1).LineStyle = wdLineStyleNone
            .Borders.Shadow = False
        End With
        With Options
            .DefaultBorderLineStyle = wdLineStyleSingle
            .DefaultBorderLineWidth = wdLineWidth050pt
            .DefaultBorderColor = wdColorAutomatic
        End With
        Selection.MoveRight Unit:=wdCharacter, Count:=1
    
    End If
End Sub
Sub Vi()
'
' Vi Macro
' Macro recorded ?01/05/2008 by Aryeh Tuchfeld
'
    Selection.InsertSymbol Font:="Times New Roman", CharacterNumber:=8730, _
        Unicode:=True
End Sub
Sub PasteUnformated()
'
' PasteUnformated Macro
' Macro recorded ?01/06/2008 by Aryeh Tuchfeld
'
    Selection.PasteAndFormat (wdPasteDefault)
End Sub
Sub ReFormat_TT_html()
'
' ReFormatTThtml Macro
' Macro recorded ?11/06/2008 by Aryeh Tuchfeld
' when prompted (to continue search from the beginning) click No.
'
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "href=""file:///D:\downloads\13_Rachamim.pdf"""
        .Replacement.Text = _
            "href=""file:///D:\downloads\13_Rachamim.pdf"" target=_blank"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
'0. use Print Folder. and aply Trim_Print_Folder macro.
'1. must use Web View.
'2. add "\" to the end of the document.
'3. Change ChangeFileOpenDirectory below.
    Selection.EndKey Unit:=wdStory
    '
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "</head>"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = False
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.EndKey Unit:=wdLine
    Selection.TypeParagraph
    Selection.TypeParagraph
    Selection.TypeText Text:="<script LANGUAGE=JavaScript src='/asp/get_site_info.js'></script>"
    Selection.TypeParagraph
    Selection.TypeText Text:="<script LANGUAGE=JavaScript src='/asp/FB_Feedback.js'></script>"
    Selection.TypeParagraph
    Selection.TypeText Text:="<script LANGUAGE=JavaScript src='t.js'></script>"
    Selection.HomeKey Unit:=wdStory
    '
Start_ReFormatTThtml_TNC_Label:
    Selection.Find.ClearFormatting
    With Selection.Find
        '.Text = "href=""t/t"
        .Text = "href=""/asp/tnc.aspx?"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If Selection.Find.Found = False Then
        Selection.HomeKey Unit:=wdStory
        GoTo Start_ReFormatTThtml_TM_Label
    End If
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = ","
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.TypeText Text:=""" onBlur=""load_MM(this, '"
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = """"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.TypeText Text:="');"""
    GoTo Start_ReFormatTThtml_TNC_Label
Start_ReFormatTThtml_TM_Label:
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "/asp/TM.aspx?"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If Selection.Find.Found = False Then
        Selection.HomeKey Unit:=wdStory
Start_Replace_pdf_Label:
        Selection.Find.ClearFormatting
        With Selection.Find
            .Text = ".pdf"""
            .Replacement.Text = ""
            .Forward = True
            .Wrap = wdFindAsk
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchKashida = False
            .MatchDiacritics = False
            .MatchAlefHamza = False
            .MatchControl = False
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        Selection.Find.Execute
        If Selection.Find.Found = False Then
            Exit Sub
        End If
        Selection.MoveRight Unit:=wdCharacter, Count:=1
        Selection.TypeText Text:=" target=""_blank"""
        GoTo Start_Replace_pdf_Label
    End If
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = ","
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.TypeText Text:=""" onClick=""load_MM(this, '"
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = """"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.TypeText Text:="');"""
    GoTo Start_ReFormatTThtml_TM_Label
End Sub
Sub Trim_Print_Folder()
'
' Macro7 Macro
' Macro recorded ?20/11/2008 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "mp3"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.EndKey Unit:=wdLine
    Selection.MoveLeft Unit:=wdWord, Count:=1, Extend:=wdExtend
    Selection.MoveRight Unit:=wdCharacter, Count:=3, Extend:=wdExtend
    Selection.Delete Unit:=wdCharacter, Count:=1
End Sub
Function FindInSameLine(str) As Boolean
    ActiveDocument.Bookmarks.Add Range:=Selection.Range, Name:="Orig_Line"
    StartLineNumber = Selection.Range.Information(wdFirstCharacterLineNumber)
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = str
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If StartLineNumber = Selection.Range.Information(wdFirstCharacterLineNumber) Then
      FindInSameLine = True
      Exit Function
    End If
    Selection.GoTo What:=wdGoToBookmark, Name:="Orig_Line"
    FindInSameLine = False
End Function
Sub GotoBeginningOfNextLine()
    Selection.HomeKey Unit:=wdLine
    Selection.MoveDown Unit:=wdLine, Count:=1
End Sub
Sub href_mp3_Print_Folder()
'
' href_mp3_Print_Folder Macro
' Macro recorded ?21/11/2008 by Aryeh Tuchfeld
'
'1. use PrintFolder Tree View and apply Trim_Print_Folder macro.
'2. use Web View.
'3. place cursor one line before first song.
'4. change ChangeFileOpenDirectory below.
    '
    Selection.HomeKey Unit:=wdLine
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "--"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.MoveRight Unit:=wdWord, Count:=1
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Selection.MoveLeft Unit:=wdCharacter, Count:=1, Extend:=wdExtend
    'Selection.Copy
    Dir_path = Selection.Text
    '
    Call GotoBeginningOfNextLine
    '
    While FindInSameLine("mp3")
        '
        Selection.EndKey Unit:=wdLine
        With Selection.Find
            .Text = "--"
            .Replacement.Text = ""
            .Forward = False
            .Wrap = wdFindAsk
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchKashida = False
            .MatchDiacritics = False
            .MatchAlefHamza = False
            .MatchControl = False
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        Selection.Find.Execute
        Selection.MoveRight Unit:=wdWord, Count:=1
        Selection.EndKey Unit:=wdLine, Extend:=wdExtend
        Selection.MoveLeft Unit:=wdCharacter, Count:=2, Extend:=wdExtend
        Selection.Copy
        Mp3FileName = Selection.Text
        '
        '
        ChangeFileOpenDirectory "E:\$MM\Music\$mp3\$?????\English\"
        Call ActiveDocument.Hyperlinks.Add(Selection.Range, Dir_path & "\" & Mp3FileName)
        Call GotoBeginningOfNextLine
    Wend
    Call GotoBeginningOfNextLine
    'ActiveDocument.Bookmarks.Add Range:=Selection.Range, Name:="a"
    'Selection.GoTo What:=wdGoToBookmark, Name:="a"
    'LineNumber = Selection.Range.Information(wdFirstCharacterLineNumber)
End Sub
Sub Paint_TH_statistics()
'
' Paint_TH_statistics Macro
' Macro recorded ?28/03/2009 by Aryeh Tuchfeld
'
    Selection.WholeStory
    Options.DefaultHighlightColorIndex = wdYellow
    Selection.Range.HighlightColorIndex = wdYellow
    Selection.HomeKey Unit:=wdStory
    Selection.Find.ClearFormatting
tyty:
    With Selection.Find
        .Text = "(100%)"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindStop
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If Selection.Find.Found = False Then
        GoTo erer1
    End If
    Selection.HomeKey Unit:=wdLine
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Options.DefaultHighlightColorIndex = wdNoHighlight
    Selection.Range.HighlightColorIndex = wdNoHighlight
    Selection.EndKey Unit:=wdLine
    GoTo tyty
erer1:
    Selection.HomeKey Unit:=wdStory
erer2:
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "(0%)"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If Selection.Find.Found = False Then
        Exit Sub
    End If
    Selection.HomeKey Unit:=wdLine
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Options.DefaultHighlightColorIndex = wdPink
    Selection.Range.HighlightColorIndex = wdPink
    Selection.EndKey Unit:=wdLine
GoTo erer2
End Sub
Sub T()
'
' T Macro
' Macro recorded ?03/05/2009 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
start_Y:
    With Selection.Find
        .Text = "????"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    If Selection.Find.Found Then
        GoTo start_Y
    End If
    Selection.MoveLeft Unit:=wdCharacter, Count:=1, Extend:=wdExtend
    Selection.Delete Unit:=wdCharacter, Count:=1
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="'"
End Sub
Sub Mark_Downloads_ListFolder()
'
' Mark_Downloads_ListFolder Macro
' Macro recorded ?26/04/2010 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "c:\"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.MoveLeft Unit:=wdCharacter, Count:=1
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Options.DefaultHighlightColorIndex = wdYellow
    Selection.Range.HighlightColorIndex = wdYellow
    Selection.EndKey Unit:=wdLine
End Sub
Sub FlagDupWords()
Dim oCol As Collection
Dim oWord As Range
Set oCol = New Collection
For Each oWord In ActiveDocument.Words
On Error GoTo Err_Handler
oCol.Add LCase(Trim(oWord)), LCase(Trim(oWord))
Err_ReEntry:
On Error GoTo 0
Next
Exit Sub
Err_Handler:
If Err.Number = 457 Then
If oWord.Characters.Last = " " Then oWord.MoveEnd wdCharacter, -1
If Len(oWord) > 4 Then oWord.HighlightColorIndex = wdBrightGreen
Resume Err_ReEntry
Else
MsgBox Err.Number & " " & Err.Description
End If
End Sub
Sub insert_ceck()
'
' insert_ceck Macro
' Macro recorded ?16/06/2011 by Aryeh Tuchfeld
'
    Selection.InsertSymbol Font:="Times New Roman", CharacterNumber:=8730, _
        Unicode:=True
End Sub
Sub Match_References()
'
' Match_References Macro
' Macro recorded ?04/01/2013 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "\[<?@>\]"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindAsk
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = True
        .MatchAlefHamza = False
        .MatchControl = True
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute
End Sub

Function Marco_Clipboard_To_String()
    ' Note: Need to add Tools->References->Microsoft Forms 2.0. Object Library (C:\Windows\system32\FM20.DLL)
    Selection.Copy
    '
    Dim MyData As DataObject
    Set MyData = New DataObject
    MyData.GetFromClipboard
    Dim sClipText As String
    sClipText = MyData.GetText
    Marco_Clipboard_To_String = sClipText
End Function
Sub MakeLinksInTable()
'
    Selection.MoveRight Unit:=wdCell
    Selection.MoveRight Unit:=wdCell
    Selection.MoveRight Unit:=wdCell
    Selection.Copy
    Link = Selection.Text
    Selection.Cut
    Selection.MoveLeft Unit:=wdCell
    Selection.MoveLeft Unit:=wdCell
    Selection.MoveLeft Unit:=wdCell
    ActiveDocument.Hyperlinks.Add Anchor:=Selection.Range, Address:=Link
    Selection.MoveDown Unit:=wdLine, Count:=1
'
End Sub
Sub MarkYellow()
'
' MarkYellow Macro
' Macro recorded ?19/06/2014 by Aryeh Tuchfeld
'
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = wdColorLightYellow
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub MarkPink()
'
' MarkPink Macro
' Macro recorded ?19/06/2014 by Aryeh Tuchfeld
'
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = 16764159
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub MarkGreen()
'
' MarkGreen Macro
' Macro recorded ?19/06/2014 by Aryeh Tuchfeld
'
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = 9043888
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub MarkBlue()
'
' MarkBlue Macro
' Macro recorded ?19/06/2014 by Aryeh Tuchfeld
'
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = 16769217
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub MarkGray()
Attribute MarkGray.VB_Description = "Macro recorded þ18/09/2014 by Aryeh Tuchfeld"
Attribute MarkGray.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Mark_Gray"
'
' Mark_Gray Macro
' Macro recorded þ18/09/2014 by Aryeh Tuchfeld
'
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = wdColorGray125
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub MarkBrown()
    With Selection.Font
        With .Shading
            .Texture = wdTextureNone
            .ForegroundPatternColor = wdColorAutomatic
            .BackgroundPatternColor = RGB(255, 222, 155)
        End With
        .Borders(1).LineStyle = wdLineStyleNone
        .Borders.Shadow = False
    End With
    With Options
        .DefaultBorderLineStyle = wdLineStyleSingle
        .DefaultBorderLineWidth = wdLineWidth050pt
        .DefaultBorderColor = wdColorAutomatic
    End With
End Sub
Sub CleanPunctuat()
Attribute CleanPunctuat.VB_Description = "Macro recorded þ27/11/2014 by Aryeh Tuchfeld"
Attribute CleanPunctuat.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Macro7"
    Set objRegEx = CreateObject("VBScript.RegExp")
    objRegEx.Global = True
    objRegEx.IgnoreCase = True
    objRegEx.Pattern = "[\s,\-;:\xa0]+"
    strNewString = objRegEx.Replace(Selection.Text, " ")
    strNewString = Trim(strNewString)
    Selection.TypeText strNewString
End Sub
Sub FormatReference()
Attribute FormatReference.VB_Description = "Macro recorded þ23/03/2015 by Aryeh Tuchfeld"
Attribute FormatReference.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.FormatReference"
'
' FormatReference Macro
' Macro recorded þ23/03/2015 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "\[[!\]]*\]"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute
    With Selection.Font
        .Name = "Times New Roman"
        .Size = 13.5
        .Bold = False
        .Italic = False
        .Underline = wdUnderlineNone
        .UnderlineColor = wdColorAutomatic
        .StrikeThrough = False
        .DoubleStrikeThrough = False
        .Outline = False
        .Emboss = False
        .Shadow = False
        .Hidden = False
        .SmallCaps = False
        .AllCaps = False
        .color = wdColorBlue
        .Engrave = False
        .Superscript = False
        .Subscript = False
        .Spacing = 0
        .Scaling = 100
        .Position = 0
        .Kerning = 0
        .Animation = wdAnimationNone
        .SizeBi = 10
        .NameBi = "David"
        .BoldBi = False
        .ItalicBi = False
    End With
    Selection.MoveRight Unit:=wdCharacter, Count:=1
End Sub
Sub Macro7_del()
Attribute Macro7_del.VB_Description = "Macro recorded þ17/06/2018 by Aryeh Tuchfeld"
Attribute Macro7_del.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Macro7"
'
' Macro7 Macro
' Macro recorded þ17/06/2018 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = ". "
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.PasteAndFormat (wdPasteDefault)
End Sub
Sub Format_TNC_verse()
Attribute Format_TNC_verse.VB_Description = "Macro recorded þ12/08/2018 by Aryeh Tuchfeld"
Attribute Format_TNC_verse.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Macro7"
'
' Macro recorded þ12/08/2018 by Aryeh Tuchfeld
'
    Selection.MoveRight Unit:=wdWord, Count:=5
    Selection.TypeText Text:=""""
    Selection.EndKey Unit:=wdLine
    Selection.MoveLeft Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:=""""
    Selection.MoveLeft Unit:=wdCharacter, Count:=1
    Selection.HomeKey Unit:=wdLine
    Selection.MoveRight Unit:=wdWord, Count:=5
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Selection.MoveLeft Unit:=wdCharacter, Count:=3, Extend:=wdExtend
    Selection.Font.Name = "Times New Roman"
    Selection.EndKey Unit:=wdLine
    Selection.MoveRight Unit:=wdCharacter, Count:=1
End Sub

Sub Format_TNC2()
'
' Macro recorded ?17/02/2019 by Aryeh Tuchfeld
'
    'Selection.Paste
    Selection.HomeKey Unit:=wdStory
    Selection.TypeParagraph
    Selection.WholeStory
    Selection.Font.Name = "David"
    Selection.Font.SizeBi = 12
    Selection.HomeKey Unit:=wdStory
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "-"
        .Replacement.Text = " "
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "\[?* "
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.BoldBi = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "["
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.BoldBi = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = " "
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.BoldBi = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    Selection.Find.MatchWildcards = True
    With Selection.Find
        .Text = "\[?*\]"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.ColorIndex = wdBlue
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    Selection.Find.MatchWildcards = True
    With Selection.Find
        .Text = "\*?*\*"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.BoldBi = True
        .Replacement.Font.color = wdColorOrange
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    Selection.Find.MatchWildcards = True
    With Selection.Find
        .Text = "#?*#"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.BoldBi = True
        .Replacement.Font.ColorIndex = wdPink
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "*"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "#"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "ãä""é"
        .Replacement.Text = "AAA"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "ùä""ù"
        .Replacement.Text = "BBB"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = """?*"""
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.Name = "Times New Roman"
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = " """
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.Name = "David"
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = """."
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .Replacement.Font.Name = "David"
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "AAA"
        .Replacement.Text = "ãä""é"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll

    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "BBB"
        .Replacement.Text = "ùä""ù"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
End Sub

Sub Format_TNC()
'
' Macro recorded ?17/02/2019 by Aryeh Tuchfeld
'
    Selection.Paste
    Selection.HomeKey Unit:=wdStory
    Selection.TypeParagraph
    Selection.WholeStory
    Selection.Font.Name = "David"
    Selection.Font.Size = 12
    Selection.HomeKey Unit:=wdStory
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "-"
        .Replacement.Text = " "
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "\[?* "
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "["
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = " "
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "\[?*\]"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "\*?*\*"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "#?*#"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "*"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = "#"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "#"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "??""?"
        .Replacement.Text = "AAA"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "??""?"
        .Replacement.Text = "AAA"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = """?*"""
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = " """
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = """."
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = """."
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "AAA"
        .Replacement.Text = "??""?"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
End Sub

Sub replace_dash()
Attribute replace_dash.VB_Description = "Macro recorded þ10/02/2019 by Aryeh Tuchfeld"
Attribute replace_dash.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Macro7"
'
' Macro7 Macro
' Macro recorded þ10/02/2019 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "-"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute
    Selection.PasteAndFormat (wdPasteDefault)
End Sub
Sub Macro7()
Attribute Macro7.VB_Description = "Macro recorded þ28/02/2019 by Aryeh Tuchfeld"
Attribute Macro7.VB_ProcData.VB_Invoke_Func = "Normal.NewMacros.Macro7"
'
' Macro7 Macro
' Macro recorded þ28/02/2019 by Aryeh Tuchfeld
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = " ""?*""."
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchKashida = False
        .MatchDiacritics = False
        .MatchAlefHamza = False
        .MatchControl = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute
    With Selection.Font
        .Name = ""
        .Size = 16
        .Bold = False
        .Italic = False
        .Underline = wdUnderlineNone
        .UnderlineColor = wdColorAutomatic
        .StrikeThrough = False
        .DoubleStrikeThrough = False
        .Outline = False
        .Emboss = False
        .Shadow = False
        .Hidden = False
        .SmallCaps = False
        .AllCaps = False
        .Engrave = False
        .Superscript = False
        .Subscript = False
        .Spacing = 0
        .Scaling = 100
        .Position = 0
        .Kerning = 0
        .Animation = wdAnimationNone
        .SizeBi = 16
        .NameBi = ""
        .ItalicBi = False
    End With
    Selection.EndKey Unit:=wdLine
End Sub
