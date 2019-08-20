var ECommerce = {
    Helper: {
        Ajax: function (method, jDto, callback) {
            var json = JSON.stringify(jDto);

            var data = new Object();
            data.Method = method;
            data.Json = json;

            $.ajax({
                method: "POST",
                url: "/api",
                data: "JSON=" + JSON.stringify(data)
            })
                .done(function (msg) {
                    if (callback) {
                        callback(msg);
                    }
                });
        }
    },
    Page: {
        Home: {

        },
        Contact: {
            Save: function () {
                var name = $("#Name").val();
                var eMail = $("#EMail").val();
                var message = $("#Message").val();
                var jDto = new Object();

                jDto.Name = name;
                jDto.EMail = eMail;
                jDto.Message = message;

                ECommerce.Helper.Ajax("SaveContact", jDto);
            }
        },
        Category: {
            Save: function () {
                var categoryId = $("#CategoryId").val();
                var productName = $("#ProductName").val();
                var productDescription = $("#ProductDescription").val();
                var jDto = new Object();
                jDto.CategoryId = categoryId;
                jDto.ProductName = productName;
                jDto.ProductDescription = productDescription;

                ECommerce.Helper.Ajax("SaveProduct", jDto, ECommerce.Page.Category.Callback_Save);
            },
            Remove: function (productId) {
                var jDto = new Object();
                jDto.ProductId = productId;
                ECommerce.Helper.Ajax("RemoveProduct", jDto, ECommerce.Page.Category.Callback_Remove);
            },
            Callback_Remove(data) {
                console.log(data);
                if (data.dynamic) ECommerce.Page.Category.List();
            },
            Callback_Save: function (data) {
                ECommerce.Page.Category.List();
                alert("Ürün başarılı şekilde kaydedildi.");
            },
            List: function (page) {
                var categoryId = $("#CategoryId").val();
                var jDto = new Object();
                jDto.CategoryId = categoryId;
                if (page) jDto.CurrentPage = page;
                //jDto.PageSize = 10;
                //jDto.PageCount = 2;
                //jDto.CurrentPage = 1;
                ECommerce.Helper.Ajax("ProductsByCategoryId", jDto, ECommerce.Page.Category.Callback_List);
            },
            Callback_List: function (data) {
                console.log(data);

                //var html = "";
                //for (var i = 0; i < data.dynamic.length; i++) {
                //    var product = data.dynamic[i];
                //    var productName = product.name;
                //    html += "- <a href='/urun/" + product.id + "'>" + productName + "</a> <input type='button' value='Sil' onclick='ECommerce.Page.Category.Remove(" + product.id + ") '/> <br />";
                //}                
                var html = "<table class='table table-striped'><thead><tr><th>Ürün Adı</th><th><button class='btn btn-success btn-sm' data-toggle='modal' data-target='#divUrunEkle' >Yeni Ürün</button></th></tr></thead><tbody>";
                for (var i = 0; i < data.dynamic.products.length; i++) {
                    var product = data.dynamic.products[i];
                    var productName = product.name;
                    html += "<tr><td>" + productName + "</td><td><input class='btn btn-danger btn-sm' type='button' value='Sil' onclick='ECommerce.Page.Category.Remove(" + product.id + ") '/> <a href='/urun/" + product.id + "' class='btn btn-warning btn-sm'>Detay</a> </td></tr>";
                }
                html += "</tbody></table>";
                var pagination = "<nav aria-label='...' ><ul class='pagination justify-content-center'>";
                var pageCount = data.dynamic.pageCount;
                var currentPage = data.dynamic.currentPage;
                var pageStart = 1;
                var pageEnd = 20;
                if (pageCount > 20 && currentPage > 10) {
                    pageStart = currentPage - 10;
                    pageEnd = currentPage + 10;
                }

                for (var i = pageStart; i <=pageEnd; i++) {
                    if (i == data.dynamic.currentPage) pagination += "<li class='page-item active'>";
                    else pagination += "<li class='page-item'>";
                    //pagination += "<span onclick='ECommerce.Page.Category.List(" + i + ");'>" + i + "</span></li>";
                    pagination += "<a class='page-link' href='#' onclick='ECommerce.Page.Category.List("+i+")'>" + i + "</a>";
                    pagination += "</li>";
                }
                pagination += "</ul></nav>";
                html += pagination;
                $("#Holder-Products").html(html);
            }
        },
        Product: {

        },
        Help: {

        }
    }
}