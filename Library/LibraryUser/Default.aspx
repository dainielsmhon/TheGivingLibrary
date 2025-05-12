<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryUser/UserMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Library.LibraryUser.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">

        <!-- הודעת פתיחה -->
        <div class="row mb-4">
            <div class="col">
                <asp:Label ID="lblWelcome" runat="server" CssClass="h2"></asp:Label>
            </div>
        </div>

        <!-- חיפוש ספרים -->
        <div class="row mb-4">
            <div class="col">
                <h3>חיפוש ספרים</h3>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="חפש ספר..."></asp:TextBox>
                <asp:Button ID="btnSearch1" runat="server" Text="חיפוש" CssClass="btn btn-primary mt-2" OnClick="btnSearch1_Click" />
                <asp:Label ID="lblSearchResult" runat="server" CssClass="text-success mt-2 d-block"></asp:Label>
                <div class="mt-3">
                    <a href="ListBook.aspx" class="btn btn-outline-secondary">לרשימת כל הספרים</a>
                    <a href="ListBorrow.aspx" class="btn btn-outline-info ml-2">לרשימת ההשאלות</a>
                </div>
            </div>
        </div>

        <!-- ספרים פופולריים -->
        <div class="row">
            <asp:Repeater ID="rptPopularBooks" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <img src='<%# ResolveUrl(Eval("ImageUrl") + "") %>' class="card-img-top" alt="ספר" />
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("BookName") %></h5>
                                <p class="card-text"><%# Eval("BookDescription") %></p>
                                <a href='<%# "AddBook.aspx?BookId=" + Eval("BookId") %>' class="btn btn-primary">קרא עוד</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- תרשימים -->
        <div class="row mb-5">
            <div class="col-md-6 text-center">
                <h4>סטטיסטיקת זמינות</h4>
                <canvas id="myPieChart" style="max-width: 150px;"></canvas>
            </div>
            <div class="col-md-6 text-center">
                <h4>10 הספרים הכי מושאלים</h4>
                <canvas id="topBooksChart" style="max-width: 150px;"></canvas>
            </div>
        </div>

        <div class="row mb-5">
            <div class="col-md-6 offset-md-3 text-center">
                <h4>5 הסופרים הכי מושאלים</h4>
                <canvas id="topAuthorsChart" style="max-width: 150px;"></canvas>
            </div>
        </div>

        <!-- טופס צור קשר -->
        <div class="row mb-5">
            <div class="col-md-8">
                <h3>צור קשר</h3>
                <div class="form-group">
                    <label>שם</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>אימייל</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>הודעה</label>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
                <asp:Button ID="btnContact" runat="server" Text="שלח הודעה" CssClass="btn btn-success" OnClick="btnContact_Click" />
                <asp:Label ID="lblContactResult" runat="server" CssClass="alert mt-3 d-block"></asp:Label>
            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <footer class="bg-dark text-white mt-5 p-4 text-center">
        כל הזכויות שמורות לספרייה הדיגיטלית © 2024
    </footer>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
    <script>
        const datalabelOptions = {
            color: '#000',
            font: { weight: 'bold' },
            formatter: (value, ctx) => {
                const label = ctx.chart.data.labels[ctx.dataIndex];
                return `${label}\n(${value})`;
            },
            anchor: 'end',
            align: 'end',
            offset: 10
        };

        function renderAllCharts() {
            var available = <%= ViewState["TotalAvailable"] ?? 0 %>;
            var borrowed = <%= ViewState["TotalBorrowed"] ?? 0 %>;

            new Chart(document.getElementById('myPieChart'), {
                type: 'pie',
                data: {
                    labels: ['ספרים זמינים', 'ספרים מושאלים'],
                    datasets: [{ data: [available, borrowed], backgroundColor: ['#28a745', '#dc3545'] }]
                },
                options: {
                    plugins: {
                        datalabels: datalabelOptions,
                        legend: { position: 'top' }
                    }
                },
                plugins: [ChartDataLabels]
            });

            new Chart(document.getElementById('topBooksChart'), {
                type: 'pie',
                data: {
                    labels: <%= ViewState["TopBooksLabels"] ?? "[]" %>,
                    datasets: [{
                        data: <%= ViewState["TopBooksValues"] ?? "[]" %>,
                        backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', '#17a2b8', '#6f42c1', '#e83e8c', '#fd7e14', '#20c997', '#343a40']
                    }]
                },
                options: {
                    plugins: {
                        datalabels: datalabelOptions,
                        legend: { position: 'top' }
                    }
                },
                plugins: [ChartDataLabels]
            });

            new Chart(document.getElementById('topAuthorsChart'), {
                type: 'pie',
                data: {
                    labels: <%= ViewState["TopAuthorsLabels"] ?? "[]" %>,
                    datasets: [{
                        data: <%= ViewState["TopAuthorsValues"] ?? "[]" %>,
                        backgroundColor: ['#007bff', '#ffc107', '#28a745', '#dc3545', '#6f42c1']
                    }]
                },
                options: {
                    plugins: {
                        datalabels: datalabelOptions,
                        legend: { position: 'top' }
                    }
                },
                plugins: [ChartDataLabels]
            });
        }

        window.onload = renderAllCharts;

        function showSuccessPush(message) {
            const toast = document.createElement('div');
            toast.innerText = message || 'הפעולה הצליחה ✅';
            toast.style.position = 'fixed';
            toast.style.top = '20px';
            toast.style.right = '20px';
            toast.style.backgroundColor = '#28a745';
            toast.style.color = 'white';
            toast.style.padding = '15px 25px';
            toast.style.borderRadius = '10px';
            toast.style.boxShadow = '0 0 10px rgba(0,0,0,0.3)';
            toast.style.zIndex = '9999';
            toast.style.opacity = '0';
            toast.style.transition = 'opacity 0.5s ease-in-out';

            document.body.appendChild(toast);

            setTimeout(() => { toast.style.opacity = '1'; }, 100);
            setTimeout(() => {
                toast.style.opacity = '0';
                setTimeout(() => toast.remove(), 500);
            }, 4000);
        }

        function showErrorPush(message) {
            const toast = document.createElement('div');
            toast.innerText = message || 'אירעה שגיאה ❌';
            toast.style.position = 'fixed';
            toast.style.top = '20px';
            toast.style.right = '20px';
            toast.style.backgroundColor = '#dc3545';
            toast.style.color = 'white';
            toast.style.padding = '15px 25px';
            toast.style.borderRadius = '10px';
            toast.style.boxShadow = '0 0 10px rgba(0,0,0,0.3)';
            toast.style.zIndex = '9999';
            toast.style.opacity = '0';
            toast.style.transition = 'opacity 0.5s ease-in-out';

            document.body.appendChild(toast);

            setTimeout(() => { toast.style.opacity = '1'; }, 100);
            setTimeout(() => {
                toast.style.opacity = '0';
                setTimeout(() => toast.remove(), 500);
            }, 4000);
        }
    </script>
</asp:Content>