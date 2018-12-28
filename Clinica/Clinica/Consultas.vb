Imports System.Data.SqlClient
Public Class Consultas
    Dim Conexion As Conexion = New Conexion()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim indice As Integer = GetGroupBoxCheckedButton(GroupBox1).TabIndex
        Salida.Label1.Text = GetGroupBoxCheckedButton(GroupBox1).Text
        If (indice >= 1 And indice <= 3) Or indice >= 5 Then
            Salida.Show()
        Else
            ComboBox2.Items.Clear()
            Console.WriteLine(Query)
            Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
            Dim Query2 As String = "SELECT ced FROM empleados"
            Console.WriteLine(Query2)
            Try
                ConSQL.Open()
                Dim cmd As SqlCommand = New SqlCommand(Query2, ConSQL)
                Dim DR As SqlDataReader = cmd.ExecuteReader()
                While DR.Read()
                    ComboBox2.Items.Add(DR(0))
                End While
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            Consulta()
        End If

    End Sub

    Public Function Consulta() As String
        Dim indice As Integer = GetGroupBoxCheckedButton(GroupBox1).TabIndex
        Salida.Label1.Text = GetGroupBoxCheckedButton(GroupBox1).Text
        Dim q As String
        Select Case indice
            Case 0
                GroupBox1.Visible = False
                Panel1.Visible = True
                q = "EXEC citas_realizadas '" + ComboBox1.Text + "', " + TextBox1.Text + ";"
            Case 1
                q = "EXEC monto_total_laboratorio"
            Case 2
                q = "EXEC pacietes_laboratorio"
            Case 3
                q = "EXEC pacientes_cita_ginecologia_pediatria"
            Case 4
                GroupBox1.Visible = False
                Panel2.Visible = True
                Dim label As String
                label = Replace(Label3.Text, vbCrLf + vbCrLf, " ")
                Console.WriteLine(label)
                Salida.Label1.Text = label + " " + ComboBox2.Text
                q = "EXEC pacientes_citas_empleado '" + ComboBox2.Text + "';"
            Case 5
                q = "EXEC salario_menor_450"
            Case 6
                q = "EXEC medicos_tratamientos"
            Case 7
                q = "EXEC empleado_citas"
        End Select
        Return q
    End Function

    Public Function Query()
        Return Consulta()
    End Function

    Private Function GetGroupBoxCheckedButton(grpb As GroupBox) As RadioButton
        Dim rButton As RadioButton = grpb.Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault()
        Return rButton
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clinica.Show()
        Salida.Close()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Salida.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GroupBox1.Visible = True
        Panel1.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Salida.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        GroupBox1.Visible = True
        Panel2.Visible = False
    End Sub
End Class