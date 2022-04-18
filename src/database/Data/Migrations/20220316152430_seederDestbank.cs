using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class seederDestbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "ATM",
                column: "Name",
                value: "ATM nội địa");

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_ABBANK",
                column: "SortIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_ACB",
                column: "SortIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_AGRIBANK",
                column: "SortIndex",
                value: 4);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_BACABANK",
                column: "SortIndex",
                value: 5);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_BIDV",
                column: "SortIndex",
                value: 6);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "WALLET",
                columns: new[] { "OtherName", "SortIndex" },
                values: new object[] { "WALLET", 36 });

            migrationBuilder.InsertData(
                table: "PaymentDestionations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DataTransactionId", "ExternalId", "Group", "Image", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "Name", "OtherName", "ParentId", "PartnerId", "SortIndex", "TimeoutAfterMinutes" },
                values: new object[,]
                {
                    { "VISAMASTERCARD", null, null, "", "VISAMASTERCARD", "", "https://s3.cloud.cmctelecom.vn/bank/637806788995070189VISAA.svg", false, null, null, "Thẻ VISA/MasterCard", "", "", "VnPay", 31, 0 },
                    { "VnPay_1PAY", null, null, "", "1PAY", "", "https://s3.cloud.cmctelecom.vn/bank/6378061615550260751PAY.svg", false, null, null, "Ví điện tử 1Pay", "1PAY", "WALLET", "VnPay", 39, 0 },
                    { "VnPay_AMEX", null, null, "", "AMEX", "", "https://s3.cloud.cmctelecom.vn/bank/637806158590292143AMEX.svg", false, null, null, "American Express", "AMEX", "ATM", "VnPay", 30, 0 },
                    { "VnPay_BIDC", null, null, "", "BIDC", "", "https://s3.cloud.cmctelecom.vn/bank/637806157091593789BIDC.svg", false, null, null, "Ngân Hàng BIDC", "BIDC", "ATM", "VnPay", 32, 0 },
                    { "VnPay_captureWallet", null, null, "", "captureWallet", "", "https://s3.cloud.cmctelecom.vn/bank/637806628704496660MomoWallet.svg", false, null, null, "Ví điện tử MoMo", "captureWallet", "WALLET", "MOMO", 46, 0 },
                    { "VnPay_DONGABANK", null, null, "", "DONGABANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806146169753496DONGABANK.svg", false, null, null, "Ngân hàng Đông Á (DongABank)", "DONGABANK", "ATM", "VnPay", 7, 0 },
                    { "VnPay_EXIMBANK", null, null, "", "EXIMBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806146480725517EXIMBANK.svg", false, null, null, "Ngân hàng EximBank", "EXIMBANK", "ATM", "VnPay", 8, 0 },
                    { "VnPay_FOXPAY", null, null, "", "FOXPAY", "", "https://s3.cloud.cmctelecom.vn/bank/637806162211903212FOXPAY.svg", false, null, null, "Ví điện tử FOXPAY", "FOXPAY", "WALLET", "VnPay", 40, 0 },
                    { "VnPay_HDBANK", null, null, "", "HDBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806146835748104HDBANK.svg", false, null, null, "Ngân hàng HDBank", "HDBANK", "ATM", "VnPay", 9, 0 },
                    { "VnPay_IVB", null, null, "", "IVB", "", "https://s3.cloud.cmctelecom.vn/bank/637806147771348722IVB.svg", false, null, null, "Ngân hàng TNHH Indovina (IVB)", "IVB", "ATM", "VnPay", 10, 0 },
                    { "VnPay_JCB", null, null, "", "JCB", "", "https://s3.cloud.cmctelecom.vn/bank/637806159890256198JCB.svg", false, null, null, "Thẻ quốc tế JCB", "VIETBANK", "VISAMASTERCARD", "VnPay", 34, 0 },
                    { "VnPay_LAOVIETBANK", null, null, "", "LAOVIETBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806157407385460LAOVIETBANK.svg", false, null, null, "Ngân hàng liên doanh Việt - Lào", "LAOVIETBANK", "ATM", "VnPay", 28, 0 },
                    { "VnPay_MASTERCARD", null, null, "", "MASTERCARD", "", "https://s3.cloud.cmctelecom.vn/bank/637806159568096097MASTERCARD.svg", false, null, null, "Thẻ quốc tế MasterCard", "MASTERCARD", "VISAMASTERCARD", "VnPay", 33, 0 },
                    { "VnPay_MBBANK", null, null, "", "MBBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806148334174391MBBANK.svg", false, null, null, "Ngân hàng thương mại cổ phần Quân đội", "MBBANK", "ATM", "VnPay", 11, 0 },
                    { "VnPay_MSBANK", null, null, "", "MSBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806148722740319MSBANK.svg", false, null, null, "Ngân hàng Hàng Hải (MSBANK)", "MSBANK", "ATM", "VnPay", 12, 0 },
                    { "VnPay_NAMABANK", null, null, "", "NAMABANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806149034305829NAMABANK.svg", false, null, null, "Ngân hàng Nam Á (NamABank)", "NAMABANK", "ATM", "VnPay", 13, 0 },
                    { "VnPay_NCB", null, null, "", "NCB", "", "https://s3.cloud.cmctelecom.vn/bank/637806149392457643NCB.svg", false, null, null, "Ngân hàng Quốc dân (NCB)", "NCB", "ATM", "VnPay", 14, 0 },
                    { "VnPay_OCB", null, null, "", "OCB", "", "https://s3.cloud.cmctelecom.vn/bank/637806149720383917OCB.svg", false, null, null, "Ngân hàng Phương Đông (OCB)", "OCB", "ATM", "VnPay", 15, 0 },
                    { "VnPay_OJB", null, null, "", "OJB", "", "https://s3.cloud.cmctelecom.vn/bank/637806150185376395OJB.svg", false, null, null, "Ngân hàng Đại Dương (OceanBank)", "OJB", "ATM", "VnPay", 16, 0 },
                    { "VnPay_PVCOMBANK", null, null, "", "PVCOMBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806150584043429PVCOMBANK.svg", false, null, null, "Ngân hàng TMCP Đại Chúng Việt Nam", "PVCOMBANK", "ATM", "VnPay", 17, 0 },
                    { "VnPay_SACOMBANK", null, null, "", "SACOMBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806150961847582SACOMBANK.svg", false, null, null, "Ngân hàng TMCP Sài Gòn Thương Tín (SacomBank)", "SACOMBANK", "ATM", "VnPay", 18, 0 },
                    { "VnPay_SAIGONBANK", null, null, "", "SAIGONBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806151334395650SAIGONBANK.svg", false, null, null, "Ngân hàng thương mại cổ phần Sài Gòn Công Thương", "SAIGONBANK", "ATM", "VnPay", 19, 0 },
                    { "VnPay_SCB", null, null, "", "SCB", "", "https://s3.cloud.cmctelecom.vn/bank/637806151750966277SCB.svg", false, null, null, "Ngân hàng TMCP Sài Gòn (SCB)", "SCB", "ATM", "VnPay", 20, 0 },
                    { "VnPay_SEABANK", null, null, "", "SEABANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806153613863188SEABANK.svg", false, null, null, "Ngân Hàng TMCP Đông Nam Á", "SEABANK", "ATM", "VnPay", 25, 0 },
                    { "VnPay_SHB", null, null, "", "SHB", "", "https://s3.cloud.cmctelecom.vn/bank/637806152081167626SHB.svg", false, null, null, "Ngân hàng Thương mại cổ phần Sài Gòn - Hà Nội(SHB)", "SHB", "ATM", "VnPay", 21, 0 },
                    { "VnPay_TECHCOMBANK", null, null, "", "TECHCOMBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806152434991789TECHCOMBANK.svg", false, null, null, "Ngân hàng Kỹ thương Việt Nam (TechcomBank)", "TECHCOMBANK", "ATM", "VnPay", 22, 0 },
                    { "VnPay_TPBANK", null, null, "", "TPBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806152821982180TPBANK.svg", false, null, null, "Ngân hàng Tiên Phong (TPBank)", "TPBANK", "ATM", "VnPay", 23, 0 },
                    { "VnPay_UPI", null, null, "", "UPI", "", "https://s3.cloud.cmctelecom.vn/bank/637806160185997487UPI.svg", false, null, null, "UnionPay International", "UPI", "VISAMASTERCARD", "VnPay", 35, 0 },
                    { "VnPay_VIB", null, null, "", "VIB", "", "https://s3.cloud.cmctelecom.vn/bank/637806154064036274VIB.svg", false, null, null, "Ngân hàng Thương mại cổ phần Quốc tế Việt Nam (VIB)", "VIB", "ATM", "VnPay", 26, 0 },
                    { "VnPay_VIETABANK", null, null, "", "VIETABANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806154670860918VIETABANK.svg", false, null, null, "Ngân hàng TMCP Việt Á", "VIETABANK", "ATM", "VnPay", 27, 0 },
                    { "VnPay_VIETBANK", null, null, "", "VIETBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806154976980402VIETBANK.svg", false, null, null, "Ngân hàng thương mại cổ phần Việt Nam Thương Tín", "VIETBANK", "ATM", "VnPay", 28, 0 },
                    { "VnPay_VIETCAPITALBANK", null, null, "", "VIETCAPITALBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806155803081396VIETCAPITALBANK.svg", false, null, null, "Ngân Hàng Bản Việt", "VIETCAPITALBANK", "ATM", "VnPay", 29, 0 },
                    { "VnPay_VIETCOMBANK", null, null, "", "VIETCOMBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806156140837146VIETCOMBANK.svg", false, null, null, "Ngân hàng Ngoại thương (Vietcombank)", "VIETCOMBANK", "ATM", "VnPay", 30, 0 },
                    { "VnPay_VIETINBANK", null, null, "", "VIETINBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806156538841413VIETINBANK.svg", false, null, null, "Ngân hàng Công thương (Vietinbank)", "VIETINBANK", "ATM", "VnPay", 31, 0 },
                    { "VnPay_VIMASS", null, null, "", "VIMASS", "", "https://s3.cloud.cmctelecom.vn/bank/637806162551187494VIMASS.svg", false, null, null, "Ví điện tử Vimass", "VIMASS", "WALLET", "VnPay", 41, 0 },
                    { "VnPay_VINID", null, null, "", "VINID", "", "https://s3.cloud.cmctelecom.vn/bank/637806162971967373VINID.svg", false, null, null, "Ví điện tử VINID", "VINID", "WALLET", "VnPay", 42, 0 },
                    { "VnPay_VISA", null, null, "", "VISA", "", "https://s3.cloud.cmctelecom.vn/bank/637806159138178191VISA.svg", false, null, null, "Thẻ quốc tế Visa", "VISA", "VISAMASTERCARD", "VnPay", 32, 0 },
                    { "VnPay_VIVIET", null, null, "", "VIVIET", "", "https://s3.cloud.cmctelecom.vn/bank/637806163321063446VIVIET.svg", false, null, null, "Ví điện tử Ví Việt", "VIVIET", "WALLET", "VnPay", 43, 0 },
                    { "VnPay_VNMART", null, null, "", "VNMART", "", "https://s3.cloud.cmctelecom.vn/bank/637806160926622446VNMART.svg", false, null, null, "Ví điện tử VnMart", "VNMART", "WALLET", "VnPay", 37, 0 },
                    { "VnPay_VNPAYQR", null, null, "", "VNPAYQR", "", "https://s3.cloud.cmctelecom.vn/bank/637806161202568121VNPAYQR.svg", false, null, null, "Cổng thanh toán VNPAYQR", "VNPAYQR", "WALLET", "VnPay", 38, 0 },
                    { "VnPay_VNPAYWallet", null, null, "", "VNPAYWallet", "", "https://s3.cloud.cmctelecom.vn/bank/637806630257309056VNPayWallet.svg", false, null, null, "Ví điện tử VNPay", "VNPAYWallet", "WALLET", "VnPay", 47, 0 },
                    { "VnPay_VNPTPAY", null, null, "", "VNPTPAY", "", "https://s3.cloud.cmctelecom.vn/bank/637806163623465015VNPTPAY.svg", false, null, null, "Ví điện tử VNPTPAY", "VNPTPAY", "WALLET", "VnPay", 44, 0 },
                    { "VnPay_VPBANK", null, null, "", "VPBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806153176785902VPBANK.svg", false, null, null, "Ngân hàng Việt Nam Thịnh vượng (VPBank)", "VPBANK", "ATM", "VnPay", 24, 0 },
                    { "VnPay_WOORIBANK", null, null, "", "WOORIBANK", "", "https://s3.cloud.cmctelecom.vn/bank/637806157725268942WOORIBANK.svg", false, null, null, "Ngân hàng TNHH MTV Woori Việt Nam", "WOORIBANK", "ATM", "VnPay", 29, 0 },
                    { "VnPay_YOLO", null, null, "", "YOLO", "", "https://s3.cloud.cmctelecom.vn/bank/637806163905567330YOLO.svg", false, null, null, "Ví điện tử YOLO", "YOLO", "WALLET", "VnPay", 45, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VISAMASTERCARD");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_1PAY");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_AMEX");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_BIDC");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_captureWallet");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_DONGABANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_EXIMBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_FOXPAY");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_HDBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_IVB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_JCB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_LAOVIETBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_MASTERCARD");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_MBBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_MSBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_NAMABANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_NCB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_OCB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_OJB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_PVCOMBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_SACOMBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_SAIGONBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_SCB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_SEABANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_SHB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_TECHCOMBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_TPBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_UPI");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIB");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIETABANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIETBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIETCAPITALBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIETCOMBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIETINBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIMASS");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VINID");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VISA");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VIVIET");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VNMART");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VNPAYQR");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VNPAYWallet");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VNPTPAY");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_VPBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_WOORIBANK");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_YOLO");

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "ATM",
                column: "Name",
                value: "Thanh toán qua ATM");

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_ABBANK",
                column: "SortIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_ACB",
                column: "SortIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_AGRIBANK",
                column: "SortIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_BACABANK",
                column: "SortIndex",
                value: 4);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_BIDV",
                column: "SortIndex",
                value: 5);

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "WALLET",
                columns: new[] { "OtherName", "SortIndex" },
                values: new object[] { "BIDV", 6 });
        }
    }
}
