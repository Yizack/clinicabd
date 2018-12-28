Imports System.Data.SqlClient
Public Class InsertarDatos
    Dim Conexion As Conexion = New Conexion()
    Dim Querystr As String
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Clinica.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Limpiar()
        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = True
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = True
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub InsertarDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboBoxes()
    End Sub

    Private Sub CargarComboBoxes()
        ComboBox1.Items.Clear()
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.Items.Clear()
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox3.Items.Clear()
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox4.Items.Clear()
        ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox5.Items.Clear()
        ComboBox5.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox6.Items.Clear()
        ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox7.Items.Clear()
        ComboBox7.DropDownStyle = ComboBoxStyle.DropDownList
        ' especialidades
        Dim Query As String = "SELECT especialidad FROM especialidades WHERE cod > 0"
        Console.WriteLine(Query)
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As SqlCommand = New SqlCommand(Query, ConSQL)
            Dim DR As SqlDataReader = cmd.ExecuteReader()
            While DR.Read()
                ComboBox1.Items.Add(DR(0))
            End While
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        ' empleados
        Dim Query2 As String = "SELECT ced FROM empleados"
        Console.WriteLine(Query2)
        Try
            ConSQL.Open()
            Dim cmd As SqlCommand = New SqlCommand(Query2, ConSQL)
            Dim DR As SqlDataReader = cmd.ExecuteReader()
            While DR.Read()
                ComboBox2.Items.Add(DR(0))
                ComboBox5.Items.Add(DR(0))
            End While
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        Dim Query3 As String = "SELECT nombre FROM cod_medicos where cod > 0 and cod_especialidad != 4"
        Console.WriteLine(Query3)
        ' medicos
        Try
            ConSQL.Open()
            Dim cmd As SqlCommand = New SqlCommand(Query3, ConSQL)
            Dim DR As SqlDataReader = cmd.ExecuteReader()
            While DR.Read()
                ComboBox3.Items.Add(DR(0))
                ComboBox4.Items.Add(DR(0))
                ComboBox6.Items.Add(DR(0))
            End While
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        ' Laboratorio
        Dim Query4 As String = "SELECT nombre FROM cod_medicos where cod > 0 and cod_especialidad = 4"
        Console.WriteLine(Query4)
        Try
            ConSQL.Open()
            Dim cmd As SqlCommand = New SqlCommand(Query4, ConSQL)
            Dim DR As SqlDataReader = cmd.ExecuteReader()
            While DR.Read()
                ComboBox7.Items.Add(DR(0))
            End While
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim query As String
        query = "EXEC spI_empleados '" + TextBox2.Text + "', '" + TextBox1.Text + "', '" + TextBox3.Text + "', '" + TextBox4.Text + "', '" + TextBox13.Text + "', '" + TextBox5.Text + "', " + TextBox6.Text + ";"
        InsertarDB(query)
    End Sub

    Private Sub Data_KeyPress1(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789-").IndexOf(e.KeyChar) = -1) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Data_KeyPress2(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If ((Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And TextBox6.Text.ToCharArray().Count(Function(c) c = ".") > 0)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub InsertarDB(ByVal query As String)
        Console.WriteLine(query)
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Using Command As SqlCommand = ConSQL.CreateCommand
                Command.CommandText = query
                Command.ExecuteNonQuery()
            End Using
            MessageBox.Show("¡Se ha registrado con éxito!")
            Limpiar()
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            Panel9.Visible = False
            Panel8.Visible = False
            Panel10.Visible = False
            Panel11.Visible = False
            CargarComboBoxes()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim query As String
        query = "EXEC spI_especialidades '" + TextBox12.Text + "';"
        InsertarDB(query)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim query As String
        Dim cod As String
        query = "SELECT cod FROM especialidades WHERE especialidad LIKE '" + ComboBox1.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            cod = cmd.ExecuteScalar()
            query = "EXEC spI_cod_medicos '" + TextBox14.Text + "', " + cod + ";"
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        InsertarDB(query)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Querystr = "SELECT * FROM empleados"
        Salida.Show()
    End Sub

    Public Function Query() As String
        Return Querystr
    End Function

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Querystr = "SELECT * FROM especialidades"
        Salida.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Querystr = "SELECT * FROM cod_medicos"
        Salida.Show()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Querystr = "SELECT * FROM pacientes"
        Salida.Show()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim query As String
        Dim fecha_nac As String
        fecha_nac = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        query = "EXEC spI_pacientes '" + TextBox7.Text + "', '" + TextBox8.Text + "', '" + TextBox9.Text + "', '" + fecha_nac + "', '" + TextBox10.Text + "', '" + TextBox11.Text + "', '" + ComboBox2.Text + "';"
        InsertarDB(query)
    End Sub

    Private Sub Data_KeyPress3(sender As Object, e As KeyPressEventArgs) Handles TextBox11.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789-").IndexOf(e.KeyChar) = -1) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = True
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim query As String
        Dim medico As String
        query = "SELECT medico FROM citas WHERE paciente = '" + TextBox19.Text + "';"
        Console.WriteLine(query)
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        If medico Like "0" Then
            Panel5.Visible = False
            Panel6.Visible = True
            TextBox15.Text = TextBox19.Text
            Dim empleado As String
            query = "SELECT empleado FROM citas WHERE paciente = '" + TextBox19.Text + "';"
            Console.WriteLine(query)
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                empleado = cmd.ExecuteScalar()
                TextBox16.Text = empleado
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
        ElseIf medico Like "" Then
            MessageBox.Show("No se ha encontrado un paciente con la cédula ingresada.")
        Else
            TextBox21.Text = TextBox19.Text
            Panel5.Visible = False
            Panel7.Visible = True
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim Query As String
        Dim medico As String
        Dim especialidad As String
        Dim fecha As String
        fecha = DateTimePicker2.Value.ToString("yyyy-MM-dd")
        Dim hora As String
        hora = DateTimePicker3.Value.ToString("HH:mm:ss")

        Query = "SELECT cod FROM cod_medicos WHERE nombre LIKE '" + ComboBox3.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(Query, ConSQL)
            medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        Query = "SELECT especialidad FROM especialidades WHERE cod = " + medico + ""
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(Query, ConSQL)
            especialidad = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        Query = "UPDATE citas SET medico = " + medico + ", especialidad = '" + especialidad + "', fecha = '" + fecha + " " + hora + "', sala = " + TextBox17.Text + " WHERE paciente LIKE '" + TextBox19.Text + "' AND medico = 0;"
        ActualizarDB(Query)
        Panel5.Visible = True
        Panel6.Visible = False
    End Sub

    Private Sub ActualizarDB(ByVal Query As String)
        InsertarDB(Query)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Panel5.Visible = True
        Panel6.Visible = False
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim query As String
        Dim fecha As String
        Dim medico As String
        fecha = DateTimePicker5.Value.ToString("yyyy-MM-dd")
        Dim hora As String
        hora = DateTimePicker4.Value.ToString("HH:mm:ss")
        query = "SELECT cod FROM cod_medicos WHERE nombre LIKE '" + ComboBox4.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        query = "EXEC spI_citas '" + TextBox21.Text + "', '" + ComboBox5.Text + "', '" + medico + "', '" + fecha + " " + hora + "', " + TextBox18.Text + ";"
        InsertarDB(query)
        Panel5.Visible = True
        Panel7.Visible = False
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Panel5.Visible = True
        Panel7.Visible = False
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Querystr = "SELECT * FROM citas WHERE paciente LIKE '" + TextBox19.Text + "'"
        Salida.Show()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim query As String
        Dim medico As String
        query = "SELECT cod FROM cod_medicos WHERE nombre LIKE '" + ComboBox7.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        query = "EXEC spI_servicio_laboratorio '" + TextBox22.Text + "', " + medico + ", '" + TextBox23.Text + "', '" + TextBox20.Text + "', " + TextBox24.Text + ";"
        InsertarDB(query)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = True
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Dim query As String
        Dim medico As String
        query = "SELECT cod FROM cod_medicos WHERE nombre LIKE '" + ComboBox6.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        query = "EXEC spI_tratamientos " + TextBox28.Text + ", " + medico + ", '" + TextBox26.Text + "';"
        InsertarDB(query)
    End Sub

    Private Sub Data_KeyPress4(sender As Object, e As KeyPressEventArgs) Handles TextBox28.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = True
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Data_KeyPress5(sender As Object, e As KeyPressEventArgs) Handles TextBox24.KeyPress
        If ((Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And TextBox24.Text.ToCharArray().Count(Function(c) c = ".") > 0)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim query As String
        Dim vacante_empleado As String
        Dim vacante_medico As String
        query = "SELECT empleados FROM vacantes WHERE cod = 1"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            vacante_empleado = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        query = "SELECT medicos FROM vacantes WHERE cod = 1"
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            vacante_medico = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        TextBox27.Text = vacante_empleado
        TextBox30.Text = vacante_empleado
        TextBox25.Text = vacante_medico
        TextBox29.Text = vacante_medico
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Panel9.Visible = False
        Panel8.Visible = False
        Panel10.Visible = True
        Panel11.Visible = False
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Panel10.Visible = False
        Panel11.Visible = True
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Dim query As String
        query = "UPDATE vacantes SET empleados = '" + TextBox30.Text + "', medicos = '" + TextBox29.Text + "' WHERE cod = 1"
        ActualizarDB(query)
    End Sub

    Private Sub Data_KeyPress6(sender As Object, e As KeyPressEventArgs) Handles TextBox29.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Data_KeyPress7(sender As Object, e As KeyPressEventArgs) Handles TextBox30.KeyPress
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789").IndexOf(e.KeyChar) = -1) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Limpiar()
        For Each tb As TextBox In Panel1.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel2.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel3.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel4.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel5.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel6.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel7.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel8.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel9.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel10.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
        For Each tb As TextBox In Panel11.Controls.OfType(Of TextBox)()
            tb.Text = String.Empty
        Next
    End Sub
End Class