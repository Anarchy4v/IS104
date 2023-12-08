Imports MySql.Data.MySqlClient

Public Class Sales
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private _userId As Integer
    Private totalAmount As Decimal = 0

    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
        End Set
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        ' Initialize Label6 with the current total amount only on the first load
        If isFirstLoad Then
            Label6.Text = "₱" & totalAmount.ToString("F2")
            isFirstLoad = False
        End If

        ' Display order details
        DisplayOrderDetails()
    End Sub

    Private displaySearchResults As Boolean = False
    Private isFirstLoad As Boolean = True

    Private Sub SearchInventory(searchText As String)
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Inventory WHERE LOWER(item_name) LIKE @searchText"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")

                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        ' Set the DataSource for POSData1 DataGridView
                        POSData1.DataSource = dataTable
                    End Using
                End Using
            End Using

            ' Call the method to display order details only when the flag is false
            If Not displaySearchResults Then
                DisplayOrderDetails()
            End If
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            displaySearchResults = True
            SearchInventory(TextBox1.Text)

            ' Check if the search results are empty
            If POSData1.Rows.Count = 0 Then
                ' If empty, call DisplayOrderDetails
                DisplayOrderDetails()
            End If
        Finally
            displaySearchResults = False
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'point of sale active
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'log out
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) 
        'cash input
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ' Add row to OrderDetails table
        If POSData1.SelectedRows.Count > 0 Then
            ' Get the selected inventory ID from POSData1
            Dim selectedInventoryId As Integer = Convert.ToInt32(POSData1.SelectedRows(0).Cells("inventory_id").Value)

            ' Prompt the user for the quantity
            Dim quantityWindow As New ModalOrderSales2()
            ' Pass the user ID to the quantity window
            quantityWindow.UserId = Me.UserId

            If quantityWindow.ShowDialog() = DialogResult.OK Then
                ' Retrieve the quantity entered by the user
                Dim orderQuantity As Integer = quantityWindow.OrderQuantity
                AddOrderDetails(selectedInventoryId, orderQuantity)

                DisplayOrderDetails()
            End If
        Else
            MessageBox.Show("Please select a medicine from POSData1 to add to the order.", "Add to Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub AddOrderDetails(inventoryId As Integer, orderQuantity As Integer)
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Retrieve the item details from the Inventory table
                Dim itemDetails As Tuple(Of String, Decimal) = GetItemDetails(inventoryId)

                ' Calculate the total for the current order
                Dim orderTotal As Decimal = orderQuantity * itemDetails.Item2

                ' Insert a new row into the OrderDetails table
                Dim query As String = "INSERT INTO OrderDetails (user_id, inventory_id, order_qty, total_price) VALUES (@userId, @inventoryId, @orderQty, @orderTotal)"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    ' Assuming you have a user ID, replace @userId with the actual user ID
                    command.Parameters.AddWithValue("@userId", UserId)
                    command.Parameters.AddWithValue("@inventoryId", inventoryId)
                    command.Parameters.AddWithValue("@orderQty", orderQuantity)
                    command.Parameters.AddWithValue("@orderTotal", orderTotal)

                    ' Execute the SQL command
                    command.ExecuteNonQuery()

                    ' Update the total amount
                    totalAmount += orderTotal

                    ' Display information in labels
                    Label2.Text = "Added " & itemDetails.Item1
                    Label3.Text = $"₱{itemDetails.Item2:F2} QTY: {orderQuantity}"
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetItemPrice(inventoryId As Integer) As Decimal
        ' Retrieve the item price from the Inventory table
        Dim itemPrice As Decimal = 0

        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Select the item price from the Inventory table based on the inventory ID
                Dim query As String = "SELECT item_price FROM Inventory WHERE inventory_id = @inventoryId"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@inventoryId", inventoryId)

                    ' Execute the SQL command
                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        itemPrice = Convert.ToDecimal(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return itemPrice
    End Function

    Private Sub DisplayOrderDetails()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Select the necessary columns from OrderDetails and join with Inventory
                Dim query As String = "SELECT OrderDetails.order_id, OrderDetails.user_id, OrderDetails.order_qty, Inventory.item_name, Inventory.item_price " &
              "FROM OrderDetails " &
              "INNER JOIN Inventory ON OrderDetails.inventory_id = Inventory.inventory_id " &
              "WHERE OrderDetails.user_id = @userId"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    ' Assuming you have a user ID, replace @userId with the actual user ID
                    command.Parameters.AddWithValue("@userId", UserId)

                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        ' Set the DataSource for POSData1 DataGridView
                        POSData1.DataSource = dataTable
                    End Using
                End Using

                ' Compute total price for the user's order
                Dim totalQuery As String = "SELECT SUM(total_price) FROM OrderDetails WHERE user_id = @userId"
                Using totalCommand As MySqlCommand = New MySqlCommand(totalQuery, connection)
                    totalCommand.Parameters.AddWithValue("@userId", UserId)

                    Dim result As Object = totalCommand.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        totalAmount = Convert.ToDecimal(result)
                        Label6.Text = "₱" & totalAmount.ToString("F2")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        ' Delete selected row from OrderDetails table
        If POSData1.SelectedRows.Count > 0 Then
            Dim selectedOrderId As Integer = Convert.ToInt32(POSData1.SelectedRows(0).Cells("order_id").Value)
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this order?", "Delete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                DeleteOrder(selectedOrderId)
                DisplayOrderDetails()
            End If
        Else
            MessageBox.Show("Please select an order from POSData1 to delete.", "Delete Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button8_Click_2(sender As Object, e As EventArgs) Handles Button8.Click
        'initialize this just to delete a row existed in my orderdetails inside POSData1
    End Sub

    Private Sub DeleteOrder(orderId As Integer)
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Retrieve the total price of the order before deletion
                Dim totalQuery As String = "SELECT total_price FROM OrderDetails WHERE order_id = @orderId"
                Using totalCommand As MySqlCommand = New MySqlCommand(totalQuery, connection)
                    totalCommand.Parameters.AddWithValue("@orderId", orderId)

                    Dim result As Object = totalCommand.ExecuteScalar()
                    Dim orderTotal As Decimal = If(result IsNot Nothing AndAlso Not DBNull.Value.Equals(result), Convert.ToDecimal(result), 0)

                    ' Delete the order from OrderDetails table
                    Dim deleteQuery As String = "DELETE FROM OrderDetails WHERE order_id = @orderId"
                    Using deleteCommand As MySqlCommand = New MySqlCommand(deleteQuery, connection)
                        deleteCommand.Parameters.AddWithValue("@orderId", orderId)

                        ' Execute the SQL command
                        deleteCommand.ExecuteNonQuery()

                        ' Update the total amount after deletion
                        totalAmount -= orderTotal
                        Label6.Text = "₱" & totalAmount.ToString("F2")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'edit order
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' Create an instance of SalesWindow and set the TotalPrice property
        Dim salesWindow As New SalesWindow(Me)
        salesWindow.TotalPrice = totalAmount ' Assuming totalAmount is the total price in your Sales form
        salesWindow.Show()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Function GetItemDetails(inventoryId As Integer) As Tuple(Of String, Decimal)
        ' Retrieve the item details (name and price) from the Inventory table
        Dim itemDetails As Tuple(Of String, Decimal) = Nothing

        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()


                Dim query As String = "SELECT item_name, item_price FROM Inventory WHERE inventory_id = @inventoryId"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@inventoryId", inventoryId)

                    ' Execute the SQL command
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Read item details from the result
                            Dim itemName As String = reader.GetString("item_name")
                            Dim itemPrice As Decimal = reader.GetDecimal("item_price")
                            itemDetails = Tuple.Create(itemName, itemPrice)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return itemDetails
    End Function

    Private Sub Sales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Assuming you have a function to retrieve order details, replace it with the actual code
        Dim orderDetails As DataTable = GetOrderDetails()

        ' Open SalesReport form and pass the order details
        Dim salesReportForm As New SalesReport(orderDetails)
        salesReportForm.Show()

        ' Close the current Dash form
        Me.Close()
    End Sub

    Private Function GetOrderDetails() As DataTable
        ' Replace this with the actual code to retrieve order details from your database or another source
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM OrderDetails"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)
                        Return dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving order details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class