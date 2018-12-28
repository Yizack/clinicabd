Imports System.Data.SqlClient
Public Class Conexion
    Public Function BaseDeDatos() As SqlConnection
        Dim bd As String
        bd = "Server = (LocalDB)\DB;Integrated Security=True;Database=clinica;"
        Dim ConSQL As New SqlConnection(bd)
        Return ConSQL
    End Function
End Class
