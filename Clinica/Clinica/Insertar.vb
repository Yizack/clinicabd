Imports System.Data.SqlClient
Public Class Insertar

    Dim Conexion As Conexion = New Conexion
    Private Sub Insertar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Limpiar()
        Select Case ComboBox1.SelectedIndex
            Case 0
                Label2.Text = "Cédula"
                Label2.Visible = True
                Label3.Text = "Nombres"
                Label3.Visible = True
                Label4.Text = "Apellidos"
                Label4.Visible = True
                Label5.Text = "Cargo"
                Label5.Visible = True
                Label8.Text = "Dirección"
                Label8.Visible = True
                Label6.Text = "Teléfono"
                Label6.Visible = True
                Label7.Text = "Salario"
                Label7.Visible = True

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Visible = True
                TextBox4.Visible = True
                TextBox7.Visible = True
                TextBox5.Visible = True
                TextBox6.Visible = True
            Case 1
                Label2.Text = "Cédula"
                Label2.Visible = True
                Label3.Text = "Nombres"
                Label3.Visible = True
                Label4.Text = "Apellidos"
                Label4.Visible = True
                Label5.Text = "Fecha de Nacimiento"
                Label5.Visible = True
                Label8.Text = "Dirección"
                Label8.Visible = True
                Label6.Text = "Teléfono"
                Label6.Visible = True
                Label7.Visible = False

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Visible = True
                DateTimePicker1.Visible = True
                TextBox4.Visible = False
                TextBox4.Text = "FILL"
                TextBox7.Visible = True
                TextBox5.Visible = True
                TextBox6.Text = "FILL"
                TextBox6.Visible = False
            Case 2
                Label2.Text = "Nombre"
                Label2.Visible = True
                Label3.Text = "Especialidad"
                Label3.Visible = True
                Label4.Visible = False
                Label5.Visible = False
                Label8.Visible = False
                Label6.Visible = False
                Label7.Visible = False

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Text = "FILL"
                TextBox3.Visible = False
                TextBox4.Text = "FILL"
                TextBox4.Visible = False
                TextBox7.Text = "FILL"
                TextBox7.Visible = False
                TextBox5.Text = "FILL"
                TextBox5.Visible = False
                TextBox6.Text = "FILL"
                TextBox6.Visible = False
            Case 3
                Label2.Text = "Cédula de Paciente"
                Label2.Visible = True
                Label3.Text = "Código de Médico"
                Label3.Visible = True
                Label4.Text = "Método"
                Label4.Visible = True
                Label5.Text = "Resultado"
                Label5.Visible = True
                Label8.Text = "Precio"
                Label8.Visible = True
                Label6.Visible = False
                Label7.Visible = False

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Visible = True
                TextBox4.Visible = True
                TextBox7.Visible = True
                TextBox5.Text = "FILL"
                TextBox5.Visible = False
                TextBox6.Text = "FILL"
                TextBox6.Visible = False
            Case 4
                Label2.Text = "Código de Cita"
                Label2.Visible = True
                Label3.Text = "Cédula de Paciente"
                Label3.Visible = True
                Label4.Text = "Cédula de Empleado"
                Label4.Visible = True
                Label5.Text = "Código de Médico"
                Label5.Visible = True
                Label8.Text = "Tipo de Cita"
                Label8.Visible = True
                Label6.Text = "Fecha"
                Label6.Visible = True
                Label9.Visible = True
                Label7.Text = "Sala"
                Label7.Visible = True

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Visible = True
                TextBox4.Visible = True
                TextBox7.Visible = True
                TextBox5.Text = "FILL"
                TextBox5.Visible = False
                DateTimePicker2.Visible = True
                DateTimePicker3.Visible = True
                TextBox6.Visible = True
            Case 5
                Label2.Text = "Código de Cita"
                Label2.Visible = True
                Label3.Text = "Código de Médico"
                Label3.Visible = True
                Label4.Text = "Descripción"
                Label4.Visible = True
                Label5.Visible = False
                Label8.Visible = False
                Label6.Visible = False
                Label7.Visible = False

                TextBox1.Visible = True
                TextBox2.Visible = True
                TextBox3.Visible = True
                TextBox4.Text = "FILL"
                TextBox4.Visible = False
                TextBox7.Text = "FILL"
                TextBox7.Visible = False
                TextBox5.Text = "FILL"
                TextBox5.Visible = False
                TextBox6.Text = "FILL"
                TextBox6.Visible = False
        End Select
        TextBox1.Tag = Label2.Text
        TextBox2.Tag = Label3.Text
        TextBox3.Tag = Label4.Text
        TextBox4.Tag = Label5.Text
        TextBox5.Tag = Label6.Text
        TextBox6.Tag = Label7.Text
        TextBox7.Tag = Label8.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As String
        Dim empty =
        Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Text.Length = 0)
        If empty.Any Then
            MessageBox.Show(String.Format("Llena porfavor los siguientes campos: {0}",
                    String.Join(", ", empty.Select(Function(txt) txt.Tag))))
        Else
            If ComboBox1.SelectedIndex >= 0 Then
                Select Case ComboBox1.SelectedIndex
                    Case 0
                        query = "INSERT INTO empleados VALUES('" _
                                                  & TextBox1.Text & "', '" _
                                                  & TextBox2.Text & "', '" _
                                                  & TextBox3.Text & "', '" _
                                                  & TextBox4.Text & "', '" _
                                                  & TextBox7.Text & "', '" _
                                                  & TextBox5.Text & "', " _
                                                  & TextBox6.Text & ");"
                        InsertarDB(query)
                    Case 1
                        TextBox4.Text = DateTimePicker1.Value.ToString("yyyy-MM-dd")
                        query = "INSERT INTO pacientes VALUES('" _
                                                  & TextBox1.Text & "', '" _
                                                  & TextBox2.Text & "', '" _
                                                  & TextBox3.Text & "', '" _
                                                  & TextBox4.Text & "', '" _
                                                  & TextBox7.Text & "', '" _
                                                  & TextBox5.Text & "');"
                        InsertarDB(query)
                    Case 2
                        Dim queryalt1 As String
                        Dim queryalt2 As String
                        queryalt1 = "INSERT INTO medicos VALUES(0 ,'" _
                                                  & TextBox2.Text & "');"
                        queryalt2 = "INSERT INTO cod_medicos VALUES(0, '" _
                                                  & TextBox1.Text & "');"

                        InsertarDB(queryalt1)
                        InsertarDB(queryalt2)
                    Case 3
                        query = "INSERT INTO servicio_laboratorio VALUES('" _
                                                  & TextBox1.Text & "', " _
                                                  & TextBox2.Text & ", '" _
                                                  & TextBox3.Text & "', '" _
                                                  & TextBox4.Text & "', " _
                                                  & TextBox7.Text & ");"
                        InsertarDB(query)
                    Case 4
                        TextBox5.Text = DateTimePicker2.Value.ToString("yyyy-MM-dd")
                        Dim hora As String = DateTimePicker3.Value.ToString("HH:mm:ss")
                        query = "INSERT INTO citas VALUES('" _
                                                  & TextBox1.Text & "', '" _
                                                  & TextBox2.Text & "', '" _
                                                  & TextBox3.Text & "', " _
                                                  & TextBox4.Text & ", '" _
                                                  & TextBox7.Text & "', '" _
                                                  & TextBox5.Text & " " & hora & "', " _
                                                  & TextBox6.Text & ");"
                        InsertarDB(query)
                    Case 5
                        query = "INSERT INTO tratamientos VALUES('" _
                                                  & TextBox1.Text & "', " _
                                                  & TextBox2.Text & ", '" _
                                                  & TextBox3.Text & "');"
                        InsertarDB(query)
                End Select
            Else
                MessageBox.Show("Selecciona una opción")
            End If
        End If
    End Sub

    Private Sub InsertarDB(ByVal query As String)
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Using Command As SqlCommand = ConSQL.CreateCommand
                Command.CommandText = query
                Command.ExecuteNonQuery()
            End Using
            MessageBox.Show("¡Se ha registrado con éxito!")
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub Limpiar()
        DateTimePicker1.Visible = False
        DateTimePicker2.Visible = False
        DateTimePicker3.Visible = False
        Label9.Visible = False
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clinica.Show()
        Me.Close()
    End Sub

    Private Sub Data_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If ((Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And TextBox6.Text.ToCharArray().Count(Function(c) c = ".") > 0)) And ComboBox1.SelectedIndex = 0 Then
            e.Handled = True
        ElseIf (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) And ComboBox1.SelectedIndex = 4 Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Data_KeyPress2(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) And ComboBox1.SelectedIndex = 3 Then
            e.Handled = True
        ElseIf (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) And ComboBox1.SelectedIndex = 5 Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Data_KeyPress3(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If ((Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And TextBox7.Text.ToCharArray().Count(Function(c) c = ".") > 0)) And ComboBox1.SelectedIndex = 3 Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Data_KeyPress4(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) And ComboBox1.SelectedIndex = 4 Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
End Class