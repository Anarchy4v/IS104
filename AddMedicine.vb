Imports MySql.Data.MySqlClient

Public Class AddMedicine
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate ComboBox1 and ComboBox2 here
        ComboBox1.Items.Add("Grams")
        ComboBox1.Items.Add("Milligrams")
        ComboBox1.Items.Add("Millilitres")
        ComboBox1.Items.Add("Liter")
        ComboBox1.Items.Add("Ratio")
        ComboBox1.Items.Add("Percentage")
        ComboBox2.Items.Add("ANTIBIOTICS")
        ComboBox2.Items.Add("GASTROINTESTINAL")
        ComboBox2.Items.Add("STOMACH")
        ComboBox2.Items.Add("ANTIHISTAMINE")
        ComboBox2.Items.Add("ASTHMA")
        ComboBox2.Items.Add("ANTI-VERTIGO")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Log out
        Dim result As DialogResult = MessageBox.Show("Cancel operation?", "Cancel Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Add medicine to the database
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Replace placeholders with actual values from your form's controls
                Dim query As String = "INSERT INTO Inventory (item_name, item_dosage, category, item_qty, item_price) VALUES (@item_name, @item_dosage, @category, @item_qty, @item_price)"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@item_name", TextBox1.Text)
                    command.Parameters.AddWithValue("@item_dosage", ComboBox1.Text)
                    command.Parameters.AddWithValue("@category", ComboBox2.Text)
                    command.Parameters.AddWithValue("@item_qty", Convert.ToInt32(TextBox3.Text))
                    command.Parameters.AddWithValue("@item_price", Convert.ToDecimal(TextBox4.Text))

                    ' Execute the SQL command
                    command.ExecuteNonQuery()

                    ' Optionally display a success message
                    MessageBox.Show("Medicine added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
