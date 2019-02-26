Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess

Namespace Dashboard_CreateGrid
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Function CreateGrid(ByVal dataSource As DashboardObjectDataSource) As GridDashboardItem

            ' Creates a grid dashboard item and specifies its data source.
            Dim grid As New GridDashboardItem()
            grid.DataSource = dataSource

            ' Creates new grid columns of the specified type and with the specified dimension or
            ' measure. Then, adds these columns to the grid's Columns collection.
            grid.Columns.Add(New GridDimensionColumn(New Dimension("CategoryName")))
            grid.Columns.Add(New GridMeasureColumn(New Measure("Extended Price")))
            grid.Columns.Add(New GridDeltaColumn(New Measure("Extended Price", SummaryType.Max), _
                                                 New Measure("Extended Price", SummaryType.Min)))
            grid.Columns.Add(New GridSparklineColumn(New Measure("Extended Price")))
            grid.SparklineArgument = New Dimension("OrderDate", DateTimeGroupInterval.MonthYear)

            Return grid
        End Function
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            ' Creates a dashboard and sets it as a currently opened dashboard in the dashboard viewer.
            dashboardViewer1.Dashboard = New Dashboard()

            ' Creates a data source and adds it to the dashboard data source collection.
            Dim dataSource As New DashboardObjectDataSource()
            dataSource.DataSource = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
            dashboardViewer1.Dashboard.DataSources.Add(dataSource)

            ' Creates a grid dashboard item with the specified data source 
            ' and adds it to the Items collection to display within the dashboard.
            Dim grid As GridDashboardItem = CreateGrid(dataSource)
            dashboardViewer1.Dashboard.Items.Add(grid)

            ' Reloads data in the data sources.
            dashboardViewer1.ReloadData()
        End Sub
    End Class
End Namespace
