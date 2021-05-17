Imports System.Xml

Public Class Library
    Public Property LibPath As String
    Public Property LibGames As New List(Of LibGameItem)
    Public Property LibCount As Integer
    Public Property LibDateCreated As String
    Public Event Reloaded()
    Public Class LibGameItem
        Public Property Name As String
        Public Property Icon As ImageSource
        Public Property BitIcon As System.Drawing.Image
        Public Property Path As String
        Public Property IconPath As String
        Public Property BigIconPath As String
        Public Property BigIcon As ImageSource
        Public Property BitBigIcon As System.Drawing.Bitmap
        Public Sub New(Title As String, IconSource As String, Source As String, BigIconSource As String)
            If IO.File.Exists(Source) Then
                Name = Title
                Path = Source
                If IO.File.Exists(IconSource) Then
                    IconPath = IconSource
                    Try
                        BitIcon = System.Drawing.Image.FromFile(IconSource)
                        Icon = Utils.ImageSourceFromBitmap(BitIcon)
                    Catch ex As Exception
                        BitIcon = Nothing
                        Icon = Nothing
                    End Try
                End If
                If IO.File.Exists(BigIconSource) Then
                    BigIconPath = BigIconSource
                    Try
                        BitBigIcon = System.Drawing.Image.FromFile(BigIconSource)
                        BigIcon = Utils.ImageSourceFromBitmap(BitBigIcon)
                    Catch ex As Exception
                        BitBigIcon = Nothing
                        BigIcon = Nothing
                    End Try
                End If
            Else
                Name = "!" & Title & "!"
                Path = Source
                BitIcon = Nothing
                Icon = Utils.ImageSourceFromBitmap(New System.Drawing.Icon(System.Drawing.SystemIcons.Warning, 256, 256).ToBitmap)
            End If
        End Sub
    End Class
    Public Shared Async Function MakeLibrary(LibraryPaths As List(Of String)) As Task(Of String)
        Dim DirectoryPath = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), My.Application.Info.AssemblyName, "Library")
        If Not IO.Directory.Exists(DirectoryPath) Then IO.Directory.CreateDirectory(DirectoryPath)
        Dim Writer As XmlWriter = XmlWriter.Create(IO.Path.Combine(DirectoryPath, "library.xml"), New XmlWriterSettings With {.Indent = True, .Async = True})
        ' Begin writing.        
        Await Writer.WriteStartDocumentAsync()
        Writer.WriteStartElement("GMLauncher")
        Writer.WriteStartElement("Library")
        Writer.WriteAttributeString("date", Now.ToString("dd/MM/yyyy HH:mm:ss"))
        Writer.WriteAttributeString("count", 0)
        Await Writer.WriteEndElementAsync
        Writer.WriteStartElement("Paths")
        For Each path In LibraryPaths
            Writer.WriteStartElement("Path")
            Dim FI As FileVersionInfo = FileVersionInfo.GetVersionInfo(path)
            Writer.WriteAttributeString("Name", FI.ProductName)
            Writer.WriteAttributeString("Source", path)
            Try
                'Using OutStream As New IO.MemoryStream
                Dim licon = TAFactory.IconPack.IconHelper.SplitGroupIcon(TAFactory.IconPack.IconHelper.ExtractIcon(path, 0)).OrderBy(Function(k) k.Width).ToArray.Reverse
                If licon.Count <> 0 Then
                    'licon(0).Save(OutStream)
                    Dim loc = IO.Path.Combine(DirectoryPath, "ICO_" & IO.Path.GetFileNameWithoutExtension(path) & ".png")
                    'System.Drawing.Image.FromStream(OutStream).Save(loc, System.Drawing.Imaging.ImageFormat.Png)
                    licon(0).ToBitmap.Save(loc, System.Drawing.Imaging.ImageFormat.Png)
                    Writer.WriteAttributeString("IconSource", loc)
                End If
                'End Using
            Catch ex As Exception
                Continue For
            End Try
            Writer.WriteAttributeString("BigIconSource", "")
            Await Writer.WriteEndElementAsync
        Next
        Await Writer.WriteEndElementAsync
        Await Writer.WriteEndElementAsync
        Await Writer.WriteEndDocumentAsync
        Writer.Close()
        Return Await Task.FromResult(IO.Path.Combine(DirectoryPath, "library.xml"))
    End Function
    Public Shared Async Function MakeLibrary() As Task(Of String)
        Dim DirectoryPath = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), My.Application.Info.AssemblyName, "Library")
        If Not IO.Directory.Exists(DirectoryPath) Then IO.Directory.CreateDirectory(DirectoryPath)
        Dim Writer As XmlWriter = XmlWriter.Create(IO.Path.Combine(DirectoryPath, "library.xml"), New XmlWriterSettings With {.Indent = True, .Async = True})
        ' Begin writing.        
        Await Writer.WriteStartDocumentAsync()
        Writer.WriteStartElement("GMLauncher")
        Writer.WriteStartElement("Library")
        Writer.WriteAttributeString("date", Now.ToString("dd/MM/yyyy HH:mm:ss"))
        Writer.WriteAttributeString("count", 0)
        Await Writer.WriteEndElementAsync
        Writer.WriteStartElement("Paths")
        'For Each path In LibraryPaths
        '    Writer.WriteStartElement("Path")
        '    Dim FI As FileVersionInfo = FileVersionInfo.GetVersionInfo(path)
        '    Writer.WriteAttributeString("Name", FI.ProductName)
        '    Writer.WriteAttributeString("Source", path)
        '    Try
        '        Using OutStream As New IO.MemoryStream
        '            Dim licon = TAFactory.IconPack.IconHelper.SplitGroupIcon(TAFactory.IconPack.IconHelper.ExtractIcon(path, 0)).OrderBy(Function(k) k.Width).ToArray.Reverse
        '            If licon.Count <> 0 Then
        '                licon(0).Save(OutStream)
        '                Dim loc = IO.Path.Combine(DirectoryPath, "ICO_" & IO.Path.GetFileNameWithoutExtension(path) & ".png")
        '                System.Drawing.Image.FromStream(OutStream).Save(loc, System.Drawing.Imaging.ImageFormat.Png)
        '                licon(0).ToBitmap.Save(loc, System.Drawing.Imaging.ImageFormat.Png)
        '                Writer.WriteAttributeString("IconSource", loc)
        '            End If
        '        End Using
        '    Catch ex As Exception
        '        Continue For
        '    End Try
        '    Writer.WriteAttributeString("BigIconSource", "")
        '    Await Writer.WriteEndElementAsync
        'Next
        'Too lazy to remove them ;)
        Await Writer.WriteEndElementAsync
        Await Writer.WriteEndElementAsync
        Await Writer.WriteEndDocumentAsync
        Writer.Close()
        Return Await Task.FromResult(IO.Path.Combine(DirectoryPath, "library.xml"))
    End Function
    Public Sub New(LibrayPath As String)
        LoadLibrary(LibrayPath)
    End Sub
    Public Sub New()
    End Sub
    Public Sub LoadLibrary(LibraryPath)
        If IO.File.Exists(LibraryPath) Then
            LibPath = LibraryPath
            Dim LibDocument As New XmlDocument()
            LibDocument.Load(LibraryPath)
            Dim attr = LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes
            LibCount = attr(1).Value
            LibDateCreated = attr(0).Value
            Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
            For Each lpath As XmlNode In lpaths
                LibGames.Add(New LibGameItem(lpath.Attributes(0).Value, lpath.Attributes(2).Value, lpath.Attributes(1).Value, lpath.Attributes(3).Value))
            Next
        End If
    End Sub
    Public Shared Async Function LoadLibraryAsync(LibraryPath) As Task(Of Library)
        Return Await Task.FromResult(Await Task.Run(Function()
                                                        If IO.File.Exists(LibraryPath) Then
                                                            Dim _Lib As New Library
                                                            _Lib.LibPath = LibraryPath
                                                            Dim LibDocument As New XmlDocument()
                                                            LibDocument.Load(LibraryPath)
                                                            Dim attr = LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes
                                                            _Lib.LibCount = attr(1).Value
                                                            _Lib.LibDateCreated = attr(0).Value
                                                            Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
                                                            For Each lpath As XmlNode In lpaths
                                                                _Lib.LibGames.Add(New LibGameItem(lpath.Attributes(0).Value, lpath.Attributes(2).Value, lpath.Attributes(1).Value, lpath.Attributes(3).Value))
                                                            Next
                                                            Return _Lib
                                                        Else
                                                            Return Nothing
                                                        End If
                                                    End Function))
    End Function
    Public Sub Reload()
        If IO.File.Exists(LibPath) Then
            LibPath = LibPath
            Dim LibDocument As New XmlDocument()
            LibDocument.Load(LibPath)
            Dim attr = LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes
            LibCount = attr(1).Value
            LibDateCreated = attr(0).Value
            Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
            LibGames.Clear()
            For Each lpath As XmlNode In lpaths
                LibGames.Add(New LibGameItem(lpath.Attributes(0).Value, lpath.Attributes(2).Value, lpath.Attributes(1).Value, lpath.Attributes(3).Value))
            Next
            RaiseEvent Reloaded()
        End If
    End Sub
    Public Sub AddGamesToLibrary(files As String())
        Dim LibDocument As New XmlDocument
        LibDocument.Load(LibPath)
        Dim Cnt = LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value
        Dim iCnt As Integer
        If Integer.TryParse(Cnt, iCnt) Then
            Cnt = iCnt
        Else
            Cnt = 0
        End If
        Cnt += files.Count
        Dim TrackNode = LibDocument.SelectSingleNode("/GMLauncher/Paths")
        For Each Path In files
            Try
                Dim xmlgame = LibDocument.CreateElement("Path")
                Dim FI As FileVersionInfo = FileVersionInfo.GetVersionInfo(Path)
                If FI.ProductName IsNot Nothing Then
                    xmlgame.SetAttribute("Name", FI.ProductName)
                Else
                    xmlgame.SetAttribute("Name", IO.Path.GetFileNameWithoutExtension(Path))
                End If
                xmlgame.SetAttribute("Source", Path)
                Using OutStream As New IO.MemoryStream
                    Try
                        Dim licon = TAFactory.IconPack.IconHelper.SplitGroupIcon(TAFactory.IconPack.IconHelper.ExtractIcon(Path, 0)).OrderBy(Function(k) k.Width).ToArray.Reverse
                        If licon.Count <> 0 Then
                            licon(0).Save(OutStream)
                            Dim loc = IO.Path.Combine(IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), My.Application.Info.AssemblyName, "Library"), "ICO_" & IO.Path.GetFileNameWithoutExtension(Path) & ".png")
                            System.Drawing.Image.FromStream(OutStream).Save(loc, System.Drawing.Imaging.ImageFormat.Png)
                            xmlgame.SetAttribute("IconSource", loc)
                        End If
                    Catch ex As Exception
                        xmlgame.SetAttribute("IconSource", "")
                    End Try
                End Using
                xmlgame.SetAttribute("BigIconSource", "")
                TrackNode.AppendChild(xmlgame)
            Catch ex As Exception
                Continue For
            End Try
        Next
        LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value = Cnt
        LibCount = Cnt
        LibDocument.Save(LibPath)
        Reload()
        TryCast(Application.Current.MainWindow, MainWindow).TriggerReload()
    End Sub
    Public Async Function AddGamesToLibraryAsync(files As String()) As Task
        Await Task.Run(Sub()
                           Dim LibDocument As New XmlDocument
                           LibDocument.Load(LibPath)
                           Dim Cnt = LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value
                           Dim iCnt As Integer
                           If Integer.TryParse(Cnt, iCnt) Then
                               Cnt = iCnt
                           Else
                               Cnt = 0
                           End If
                           Cnt += files.Count
                           Dim TrackNode = LibDocument.SelectSingleNode("/GMLauncher/Paths")
                           For Each Path In files
                               Try
                                   Dim xmlgame = LibDocument.CreateElement("Path")
                                   Dim FI As FileVersionInfo = FileVersionInfo.GetVersionInfo(Path)
                                   If FI.ProductName IsNot Nothing Then
                                       xmlgame.SetAttribute("Name", FI.ProductName)
                                   Else
                                       xmlgame.SetAttribute("Name", IO.Path.GetFileNameWithoutExtension(Path))
                                   End If
                                   xmlgame.SetAttribute("Source", Path)
                                   Using OutStream As New IO.MemoryStream
                                       Try
                                           Dim licon = TAFactory.IconPack.IconHelper.SplitGroupIcon(TAFactory.IconPack.IconHelper.ExtractIcon(Path, 0)).OrderBy(Function(k) k.Width).ToArray.Reverse
                                           If licon.Count <> 0 Then
                                               licon(0).Save(OutStream)
                                               Dim loc = IO.Path.Combine(IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), My.Application.Info.AssemblyName, "Library"), "ICO_" & IO.Path.GetFileNameWithoutExtension(Path) & ".png")
                                               System.Drawing.Image.FromStream(OutStream).Save(loc, System.Drawing.Imaging.ImageFormat.Png)
                                               xmlgame.SetAttribute("IconSource", loc)
                                           End If
                                       Catch ex As Exception
                                           xmlgame.SetAttribute("IconSource", "")
                                       End Try
                                   End Using
                                   xmlgame.SetAttribute("BigIconSource", "")
                                   TrackNode.AppendChild(xmlgame)
                               Catch ex As Exception
                                   Continue For
                               End Try
                           Next
                           LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value = Cnt
                           LibCount = Cnt
                           LibDocument.Save(LibPath)
                       End Sub)
        Reload()
        TryCast(Application.Current.MainWindow, MainWindow).TriggerReload()
    End Function
    Public Sub ScanLibrary()
        Dim LibDocument As New XmlDocument
        LibDocument.Load(LibPath)
        Dim Cnt As Integer
        Dim GameParentNode = LibDocument.SelectSingleNode("/GMLauncher/Paths")
        Dim TrackNode = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
        For Each game As XmlNode In TrackNode
            If Not IO.File.Exists(game.Attributes(1).Value) Then
                Try
                    IO.File.Delete(game.Attributes(2).Value)
                Catch ex As Exception
                End Try
                GameParentNode.RemoveChild(game)
            End If
        Next
        Cnt = LibDocument.SelectNodes("/GMLauncher/Paths/Path").Count
        LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value = Cnt
        LibCount = Cnt
        LibDocument.Save(LibPath)
        Reload()
        TryCast(Application.Current.MainWindow, MainWindow).TriggerReload()
    End Sub
    Public Sub RemoveFromLibrary(source As String)
        Dim LibDocument As New XmlDocument
        LibDocument.Load(LibPath)
        Dim Cnt As Integer
        Dim GameParentNode = LibDocument.SelectSingleNode("/GMLauncher/Paths")
        Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
        Dim ToRemoveGames As New List(Of LibGameItem)
        For Each lpath As XmlNode In lpaths
            ToRemoveGames.Add(New LibGameItem(lpath.Attributes(0).Value, lpath.Attributes(2).Value, lpath.Attributes(1).Value, lpath.Attributes(3).Value))
        Next
        Dim itm = ToRemoveGames.FirstOrDefault(Function(k) k.Path = source)
        GameParentNode.RemoveChild(lpaths.Item(ToRemoveGames.IndexOf(itm)))
        Cnt = LibDocument.SelectNodes("/GMLauncher/Paths/Path").Count
        LibDocument.SelectSingleNode("/GMLauncher/Library").Attributes(1).Value = Cnt
        LibCount = Cnt
        LibDocument.Save(LibPath)
        Try
            IO.File.Delete(itm.IconPath)
        Catch ex As Exception
        End Try
        Reload()
        TryCast(Application.Current.MainWindow, MainWindow).TriggerReload()
    End Sub
    Public Sub UpdateLibrary(Game As LibGameItem, Idx As Integer)
        Dim LibDocument As New XmlDocument()
        LibDocument.Load(LibPath)
        Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
        Dim n_node = lpaths.Item(Idx)
        n_node.Attributes.Item(0).Value = Game.Name
        Try
            System.Drawing.Image.FromFile(Game.IconPath).Save(n_node.Attributes.Item(2).Value, System.Drawing.Imaging.ImageFormat.Png)
        Catch ex As Exception
            n_node.Attributes.Item(2).Value = Game.IconPath
        End Try
        If IO.File.Exists(Game.BigIconPath) Then
            n_node.Attributes.Item(3).Value = Game.BigIconPath
        End If
        n_node.Attributes.Item(1).Value = Game.Path
        LibDocument.Save(LibPath)
        Reload()
    End Sub
    Public Async Function UpdateLibraryAsync(Game As LibGameItem, Idx As Integer) As Task
        Await Task.Run(Sub()
                           Dim LibDocument As New XmlDocument()
                           LibDocument.Load(LibPath)
                           Dim lpaths = LibDocument.SelectNodes("/GMLauncher/Paths/Path")
                           Dim n_node = lpaths.Item(Idx)
                           n_node.Attributes.Item(0).Value = Game.Name
                           Try
                               System.Drawing.Image.FromFile(Game.IconPath).Save(n_node.Attributes.Item(2).Value, System.Drawing.Imaging.ImageFormat.Png)
                           Catch ex As Exception
                               n_node.Attributes.Item(2).Value = Game.IconPath
                           End Try
                           If IO.File.Exists(Game.BigIconPath) Then
                               n_node.Attributes.Item(3).Value = Game.BigIconPath
                           End If
                           n_node.Attributes.Item(1).Value = Game.Path
                           LibDocument.Save(LibPath)
                       End Sub)
        Reload()
    End Function
End Class
