Imports MySql.Data.MySqlClient

Public Class Dash
    Dim random As New Random()
    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False
    Private userEmail As String

    Public Property UserEmailProperty As String
        Get
            Return userEmail
        End Get
        Set(value As String)
            userEmail = value
            Label2.Text = userEmail
        End Set
    End Property

    Private Sub LoadMedicines()
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT med_id, med_name, med_dosage, med_QTY, med_price, UnitName FROM medicine JOIN DosageUnits ON medicine.DosageUnitID = DosageUnits.ID;"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        DataGridView1.Rows.Clear()

                        For Each row As DataRow In dataTable.Rows()
                            Dim rowIndex As Integer = DataGridView1.Rows.Add()
                            DataGridView1.Rows(rowIndex).Cells("ID").Value = row("med_id").ToString()
                            DataGridView1.Rows(rowIndex).Cells("Name").Value = row("med_name").ToString()
                            DataGridView1.Rows(rowIndex).Cells("Dosage").Value = row("med_dosage").ToString()
                            DataGridView1.Rows(rowIndex).Cells("Quantity").Value = row("med_QTY").ToString()
                            DataGridView1.Rows(rowIndex).Cells("Price").Value = row("med_price").ToString()
                            DataGridView1.Rows(rowIndex).Cells("Unit").Value = row("UnitName").ToString()
                        Next
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        ' tarantadong error di pa rin ako pinili T_T
        DataGridView1.Columns.Add("ID", "ID")
        DataGridView1.Columns.Add("Name", "Name")
        DataGridView1.Columns.Add("Dosage", "Dosage")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("Price", "Price")
        DataGridView1.Columns.Add("Unit", "Unit")
        'load them in shit
        LoadMedicines()
        'I add this because of fatigue for nothing.
        Me.AcceptButton = Nothing
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard active
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        'idk this
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    'dashboard text

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        'idk who's the data source?
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'sales report
        Dim salesReport As New SalesReport()
        salesReport.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        'settings
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub


    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs)
        'search box???
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)
        'data grid view
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Generate syempre live count haha
        Dim randomNumber As Integer = random.Next(1, 301)
        Dim randomNumber1 As Integer = random.Next(1, 401)
        Dim randomNumber2 As Integer = random.Next(1, 501)
        Dim randomNumber3 As Integer = random.Next(1, 201)

        Label7.Text = randomNumber.ToString()
        Label8.Text = randomNumber1.ToString()
        Label9.Text = randomNumber2.ToString()
        Label10.Text = randomNumber3.ToString()
    End Sub
End Class