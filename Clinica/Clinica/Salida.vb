Imports System.Data.SqlClient
Public Class Salida
    Dim Conexion As Conexion = New Conexion()
    Dim Clinica As Consultas = New Consultas()
    Dim Query As String
    Private Sub Salida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Consultas.Visible Then
            Query = Consultas.Query
        Else
            Query = InsertarDatos.Query
        End If
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Console.WriteLine(Query)
        Try
            Dim dt As DataTable = New DataTable()
            ConSQL.Open()
            Using dad As New SqlDataAdapter(Query, ConSQL)
                dad.Fill(dt)
            End Using
            DataGridView1.DataSource = dt
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class