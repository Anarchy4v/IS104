Imports MySql.Data.MySqlClient
Imports PharmacyandMedicine.SalesWindow

Public Class ModalOrderSales2
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Public Property OrderDetails As OrderDetails
    Private ReadOnly salesId As Integer


    ' Add a parameter to the constructor for sales_id
    Public Sub New(itemName As String, initialQuantity As Integer, salesId As Integer, itemDosage As String)
        InitializeComponent()
        OrderDetails = New OrderDetails()

        ' Set initial values
        OrderDetails.ItemName = itemName
        OrderDetails.Quantity = initialQuantity
        OrderDetails.Dosage = itemDosage
        Me.salesId = salesId ' Store the sales_id
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ModalOrderSales2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display initial values
        TextBox1.Text = OrderDetails.Quantity.ToString()

        ' Retrieve item price when the form loads
        Dim itemPrice As Decimal = RetrieveItemPrice("")

        ' Set the price based on your logic (e.g., quantity * item_price)
        Me.OrderDetails.Price = Me.OrderDetails.Quantity * itemPrice
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validate and get the quantity input
        If Integer.TryParse(TextBox1.Text, Me.OrderDetails.Quantity) Then
            ' Retrieve dosage and price information from the INVENTORY table
            Dim itemDosage As String = RetrieveDosage(OrderDetails.ItemName)
            Dim itemPrice As Decimal = RetrieveItemPrice(OrderDetails.ItemName)
            Me.OrderDetails.Price = Me.OrderDetails.Quantity * itemPrice ' Use the retrieved item price

            ' Insert into compute_sales table
            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "INSERT INTO compute_sales (sales_name, sales_qty, sales_price, sales_id, sales_category, sales_dosage) " &
                    "VALUES (@sales_name, @sales_qty, @sales_price, @sales_id, @sales_category, @sales_dosage)"

                    Using cmd As New MySqlCommand(query, connection)
                        cmd.Parameters.AddWithValue("@sales_name", OrderDetails.ItemName)
                        cmd.Parameters.AddWithValue("@sales_qty", OrderDetails.Quantity)
                        cmd.Parameters.AddWithValue("@sales_price", OrderDetails.Price)
                        cmd.Parameters.AddWithValue("@sales_id", salesId)
                        cmd.Parameters.AddWithValue("@sales_category", OrderDetails.Quantity.ToString()) ' Use quantity as category
                        cmd.Parameters.AddWithValue("@sales_dosage", itemDosage) ' Use the retrieved dosage

                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Medicine added successfully to Ordering Details.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error inserting data into compute_sales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Close()
    End Sub

    Private Function RetrieveDosage(itemName As String) As String
        Dim dosage As String = String.Empty

        ' Retrieve dosage information
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT item_dosage FROM inventory WHERE item_name = @item_name"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@item_name", itemName)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            dosage = Convert.ToString(reader("item_dosage"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving dosage information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dosage
    End Function

    Private Function RetrieveItemPrice(itemName As String) As Decimal
        Dim itemPrice As Decimal = 0

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Directly query item_price without using a parameter
                Dim query As String = $"SELECT item_price FROM inventory WHERE item_name = '{itemName}'"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            itemPrice = Convert.ToDecimal(reader("item_price"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving item price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return itemPrice
    End Function
End Class
