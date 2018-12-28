Imports System.Data.SqlClient
Public Class GestionarDatos
    Dim Conexion As Conexion = New Conexion()
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Clinica.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Limpiar()
        CargarComboboxes()
        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ComboBox1.Text Like "" Then
            MessageBox.Show("ERROR: El campo no puede ir vacío.")
        Else
            TextBox10.Text = ComboBox1.Text
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = True
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
        End If
    End Sub

    Private Sub CargarComboboxes()
        ComboBox1.Items.Clear()
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.Items.Clear()
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ' empleados
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Dim Query As String = "SELECT ced FROM empleados"
        Console.WriteLine(Query)
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
        Dim Query3 As String = "SELECT cod FROM cod_medicos where cod > 0"
        Console.WriteLine(Query3)
        ' medicos
        Try
            ConSQL.Open()
            Dim cmd As SqlCommand = New SqlCommand(Query3, ConSQL)
            Dim DR As SqlDataReader = cmd.ExecuteReader()
            While DR.Read()
                ComboBox2.Items.Add(DR(0))
            End While
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub GestionarDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboboxes()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox1.Text Like "" Then
            MessageBox.Show("ERROR: El campo no puede ir vacío.")
        Else
            TextBox1.Text = ComboBox1.Text
            Dim query As String
            Dim cargo As String
            Dim direccion As String
            Dim telefono As String
            Dim salario As String
            query = "SELECT cargo FROM empleados WHERE ced LIKE '" + ComboBox1.Text + "'"
            Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                cargo = cmd.ExecuteScalar()
                TextBox2.Text = cargo
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT direccion FROM empleados WHERE ced LIKE '" + ComboBox1.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                direccion = cmd.ExecuteScalar()
                TextBox3.Text = direccion
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT telefono FROM empleados WHERE ced LIKE '" + ComboBox1.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                telefono = cmd.ExecuteScalar()
                TextBox4.Text = telefono
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT salario FROM empleados WHERE ced LIKE '" + ComboBox1.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                salario = cmd.ExecuteScalar()
                TextBox5.Text = salario
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim query As String
        query = "EXEC spU_empleados '" + ComboBox1.Text + "', '" + TextBox2.Text + "', '" + TextBox3.Text + "', '" + TextBox4.Text + "', " + TextBox5.Text + ";"
        ActualizarDB(query)
    End Sub

    Private Sub ActualizarDB(ByVal query As String)
        Console.WriteLine(query)
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Using Command As SqlCommand = ConSQL.CreateCommand
                Command.CommandText = query
                Command.ExecuteNonQuery()
            End Using
            MessageBox.Show("¡Se ha registrado con éxito!")
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            CargarComboboxes()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel3.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim query As String
        query = "EXEC spD_empleados '" + ComboBox1.Text + "';"
        EliminarDB(query)
    End Sub

    Private Sub EliminarDB(ByVal query As String)
        ActualizarDB(query)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Limpiar()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim query As String
        Dim ced As String
        query = "SELECT ced FROM pacientes WHERE ced LIKE '" + TextBox6.Text + "'"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            ced = cmd.ExecuteScalar()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            ConSQL.Close()
        End Try
        If TextBox6.Text Like "" Or ced Like "" Then
            MessageBox.Show("ERROR: No se encuentra la cédula.")
        Else
            TextBox7.Text = TextBox6.Text
            Dim nombre As String
            Dim apellido As String
            Dim direccion As String
            Dim telefono As String
            query = "SELECT nombres FROM pacientes WHERE ced LIKE '" + TextBox6.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                nombre = cmd.ExecuteScalar()
                TextBox8.Text = nombre
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT apellidos FROM pacientes WHERE ced LIKE '" + TextBox6.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                apellido = cmd.ExecuteScalar()
                TextBox9.Text = apellido
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT direccion FROM pacientes WHERE ced LIKE '" + TextBox6.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                direccion = cmd.ExecuteScalar()
                TextBox11.Text = direccion
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            query = "SELECT telefono FROM pacientes WHERE ced LIKE '" + TextBox6.Text + "'"
            Try
                ConSQL.Open()
                Dim cmd As New SqlCommand(query, ConSQL)
                telefono = cmd.ExecuteScalar()
                TextBox12.Text = telefono
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                ConSQL.Close()
            End Try
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = True
            Panel6.Visible = False
            Panel7.Visible = False
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim query As String
        query = "EXEC spU_pacientes '" + TextBox6.Text + "', '" + TextBox11.Text + "', '" + TextBox12.Text + "';"
        ActualizarDB(query)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Limpiar()
        CargarComboboxes()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim query As String
        Dim nombre As String
        query = "SELECT nombre FROM cod_medicos WHERE cod = " + ComboBox2.Text + ";"
        Dim ConSQL As SqlConnection = Conexion.BaseDeDatos
        Try
            ConSQL.Open()
            Dim cmd As New SqlCommand(query, ConSQL)
            nombre = cmd.ExecuteScalar()
            TextBox13.Text = nombre
        Catch ex As SqlException
            MessageBox.Show("ERROR: El campo no puede estar vacío.")
            nombre = ""
        Finally
            ConSQL.Close()
        End Try
        If nombre <> "" Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = True
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim query As String
        query = "EXEC spD_medicos " + ComboBox2.Text + ";"
        EliminarDB(query)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
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
    End Sub
End Class