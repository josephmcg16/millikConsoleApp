Imports System
Imports System.Threading.Channels
Imports NationalInstruments.Visa
Imports Ivi.Visa

Module Program
    Private _Session As MessageBasedSession
    Private ReadOnly ResourceName As String = "TCPIP0::192.168.0.87::1000::SOCKET"
    Private ReadOnly ChannelCommand As String = "PROB:COUN?"
    Private asyncHandle As IVisaAsyncResult = Nothing
    'Private ChannelCommand As String = "MEAS:TEMP1? 1,C,460,NORM"
    'Private ChannelCommand As String = "MEAS:TEMP1? 2,C,460,NORM"
    'Private ChannelCommand As String = "MEAS:TEMP1? 3,C,460,NORM"
    'Private ChannelCommand As String = "MEAS:TEMP1? 4,C,460,NORM"
    'Private ChannelCommand As String = "MEAS:TEMP1? 5,C,460,NORM"

    Friend ReadOnly Property Session As MessageBasedSession
        Get
            If IsNothing(_Session) Then
                _Session = New TcpipSocket(resourceName:=ResourceName)
            End If
            Return _Session
        End Get
    End Property

    Sub Main(args As String())
        Dim dattimestamp As Date = Now
        Dim strreadstring As String
        Session.RawIO.Write(buffer:=ChannelCommand & vbCrLf)
        Session.RawIO.Write(buffer:=ChannelCommand & vbCrLf)
        asyncHandle = Session.RawIO.BeginRead(1024, New VisaAsyncCallback(AddressOf OnReadComplete))
        strreadstring = Session.RawIO.EndReadString(asyncHandle)
        Console.WriteLine("Failed to read milliK comms...")

    End Sub

    Private Sub OnReadComplete(result As IVisaAsyncResult)
        Dim responseString = Session.RawIO.EndReadString(result)
        Console.WriteLine(responseString)
    End Sub
End Module
