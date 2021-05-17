Imports Microsoft.Win32

Public Class Utils
    Public Class PeakItem
        Public Property Master As Single
        Public Property Left As Single
        Public Property Right As Single
        Public Sub New(_Master As Single, _Left As Single, _Right As Single)
            Master = _Master
            Left = _Left
            Right = _Right
        End Sub
    End Class
    Public Shared Property DefaultMargin As New Thickness(10, 10, 10, 10)
    Public Shared Property DAnimOneOneHundred As New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(100)))
    Public Shared Property DAnimZeroOneHundred As New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(100)))
    Public Shared Property DAnimZeroFifty As New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(50)))
    Public Shared Property S_LogOn As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.ps4_log_on.wav")
    Public Shared Property S_Navigation As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.p_nav.wav")
    Public Shared Property S_Select As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.p_select.wav")
    Public Shared Property S_Back As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.p_back.wav")
    Public Shared Property S_Error As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.p_error.wav")
    Public Shared Property S_BGM As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.SOUND.mp3")
    Public Shared Property S_Xbox360Nav As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.Xbox360Nav.wav")
    Public Shared Property S_Xbox360Select As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.Xbox360Select.wav")
    Public Shared Property S_Xbox360Home As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.Xbox360Home.wav")
    Public Shared Property S_Xbox360GameSelect As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.Xbox360GameSelect.wav")
    Public Shared Property S_LogOn_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_LogOn)
    Public Shared Property S_Navigation_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Navigation)
    Public Shared Property S_Select_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Select)
    Public Shared Property S_Back_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Back)
    Public Shared Property S_Error_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Error)
    Public Shared Property S_Xbox360Nav_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Xbox360Nav)
    Public Shared Property S_Xbox360Select_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Xbox360Select)
    Public Shared Property S_Xbox360Home_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Xbox360Home)
    Public Shared Property S_Xbox360GameSelect_Player As System.Media.SoundPlayer = New System.Media.SoundPlayer(S_Xbox360GameSelect)
    Public Shared Property OSD_NOTIFY As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_notify.png"))
    Public Shared Property OSD_NOTIFY1 As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_notify_1.png"))
    Public Shared Property OSD_NOTIFY2 As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_notify_2.png"))
    Public Shared Property OSD_NOTIFY3 As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_notify_3.png"))
    Public Shared Property OSD_NOTIFY4 As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_notify_4.png"))
    Public Shared Property OSD_SETTINGS As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_settings.png"))
    Public Shared Property OSD_SHUTDOWN As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_power.png"))
    Public Shared Property OSD_BACKGROUND As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_background.png"))
    Public Shared Property OSD_GAMESSETTINGS As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_gamesettings.png"))
    Public Shared Property OSD_UPDATE As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_update.png"))
    Public Shared Property OSD_DATETIMESETTINGS As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_datetimesettings.png"))
    Public Shared Property OSD_SOUNDSETTINGS As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_soundsettings.png"))
    Public Shared Property OSD_GAME As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_game.png"))
    Public Shared Property OSD_DISCORD As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_discord.png"))
    Public Shared Property OSD_GMLAUNCHER As ImageSource = New BitmapImage(New Uri("pack://application:,,,/GMLauncher;component/Res/osd_gmlauncher.png"))
    Public Shared Property FILTER_AUDIO As String = "Supported Files|*.mp3;*.m4a;*.wav;*.aiff;*.mp2;*.mp1;*.ogg;*.wma;*.alac;*.webm|All files|*.*"
    Public Shared Property FILTER_IMAGE As String = "Supported Files|*.jpg;*.jpeg;*.png;*.bmp;*.ico"
    Public Shared Property FILTER_EXE As String = "Supported Files|*.exe"
    Public Shared Function GetInstalledApps() As List(Of AppItem)
        Dim RetList As New List(Of AppItem)
        ' HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products
        Dim Software As String = Nothing
        'The registry key will be held in a string SoftwareKey.
        Dim SoftwareKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
        Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey(SoftwareKey)
        If rk Is Nothing Then
            Using rk
                For Each skName In rk.GetSubKeyNames
                    'Get sub keys
                    Dim name = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("DisplayName")
                    'Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("DisplayName")
                    Dim installocation = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("InstallLocation")
                    'InstallProperties
                    Dim publisher = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("Publisher")
                    Dim uninstallString = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("UninstallString")
                    'Add the Software information to lstPrograms
                    If name IsNot Nothing Then
                        ' Add it to lstPrograms, a listview that will hold our Software List.
                        RetList.Add(New AppItem(name, installocation, publisher, uninstallString))
                    End If
                    'next
                Next
            End Using
        Else
            Dim BaseKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
            Dim rk2 As RegistryKey = BaseKey.OpenSubKey(SoftwareKey)
            If rk2 IsNot Nothing Then
                Using rk2
                    For Each skName In rk2.GetSubKeyNames
                        'Get sub keys
                        Dim name = BaseKey.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("DisplayName")
                        'Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("DisplayName")
                        Dim installocation = BaseKey.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("InstallLocation")
                        'InstallProperties
                        Dim publisher = BaseKey.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("Publisher")
                        Dim uninstallString = BaseKey.OpenSubKey(SoftwareKey).OpenSubKey(skName).GetValue("UninstallString")
                        'Add the Software information to lstPrograms
                        If name IsNot Nothing Then
                            ' Add it to lstPrograms, a listview that will hold our Software List.
                            RetList.Add(New AppItem(name, installocation, publisher, uninstallString))
                        End If
                        'next
                    Next
                End Using
            End If
        End If
        Return RetList
    End Function
    Public Class AppItem
        Public Property DisplayName As String
        Public Property InstallLocation As String
        Public Property Publisher As String
        Public Property UninstallString As String
        Public Sub New(Name As String, Location As String, Author As String, UnInstallStr As String)
            DisplayName = Name
            InstallLocation = Location
            Publisher = Author
            UninstallString = UnInstallStr
        End Sub
    End Class
    Public Shared Function ImageSourceFromBitmap(ByVal bmp As System.Drawing.Bitmap, Optional ChangeRes As Boolean = False, Optional ResX As Integer = 0, Optional ResY As Integer = 0) As ImageSource
        If ChangeRes = False Then
            Try
                Dim handle = bmp.GetHbitmap()
                Try
                    Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
                Finally
                    DeleteObject(handle)
                End Try
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Try
                Dim ResBmp As New System.Drawing.Bitmap(bmp, ResX, ResY)
                Dim handle = ResBmp.GetHbitmap()
                Try
                    Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
                Finally
                    DeleteObject(handle)
                End Try
            Catch ex As Exception
                Return Nothing
            End Try
        End If
    End Function
    <Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint:="DeleteObject")>
    Public Shared Function DeleteObject(
<Runtime.InteropServices.[In]> ByVal hObject As IntPtr) As Boolean
    End Function
    ''' <summary>
    ''' Artist, Title, Album, Year, TrackNum, Genres, Lyrics, Bitrates
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function GetSongInfo(path As String) As String()
        Try
            Dim Tag = TagLib.File.Create(path)
            Dim Artist = Tag.Tag.JoinedPerformers
            Dim Title = Tag.Tag.Title
            Dim Album = Tag.Tag.Album
            Dim Year = Tag.Tag.Year
            Dim TrackNum = Tag.Tag.TrackCount
            Dim Lyrics = Tag.Tag.Lyrics
            Dim Genres = Tag.Tag.Genres
            Dim Bitrates = Tag.Properties.AudioBitrate
            Dim CompressedInfo As String()
            CompressedInfo = {Artist, Title, Album, Year, TrackNum, String.Join(";", Genres), Lyrics, Bitrates}
            Return CompressedInfo
        Catch ex As Exception
            Return {"Not Available", "Not Available", "Not Available", 0, 0, "Not Available", "Not Available", 0}
        End Try
    End Function
    Public Shared Function GetAlbumArt(path As String) As System.Drawing.Image
        Try
            Dim Tag = TagLib.File.Create(path)
            If Tag.Tag.Pictures.Length >= 1 Then
                Dim bin = DirectCast(Tag.Tag.Pictures(0).Data.Data, Byte())
                Dim Cover = System.Drawing.Image.FromStream(New IO.MemoryStream(bin))
                Return Cover
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub FindInstalls(ByVal regKey As RegistryKey, ByVal keys As List(Of String), ByVal installed As List(Of Utils.AppItem))
        For Each key As String In keys

            Using rk As RegistryKey = regKey.OpenSubKey(key)

                If rk Is Nothing Then
                    Continue For
                End If

                For Each skName As String In rk.GetSubKeyNames()

                    Using sk As RegistryKey = rk.OpenSubKey(skName)

                        Try
                            installed.Add(New AppItem(Convert.ToString(sk.GetValue("DisplayName")), Convert.ToString(sk.GetValue("InstallLocation")), Convert.ToString(sk.GetValue("Publisher")), Convert.ToString(sk.GetValue("UninstallString"))))
                        Catch ex As Exception
                        End Try
                    End Using
                Next
            End Using
        Next
    End Sub
    Public Enum StartUpType
        Wave = 0
        Particles = 1
        PS1 = 2
        PS2 = 3
        PS3 = 4
        Xbox = 5
        Xbox360Old = 6
        Xbox360New = 7
        XboxOneX = 8
        CyberPunk = 9
    End Enum
End Class
