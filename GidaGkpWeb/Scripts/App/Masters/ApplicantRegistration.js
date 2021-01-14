﻿/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    $('#usertype').on('change', function (e) {
        if (this.value == 'Ex Applicant') {
            $('#divExAlotee').css('display', 'block');
        }
        else {
            $('#SchemeType').val('');
            $('#SchemeName').val('');
            $('#SectorName').val('');
            $('#divExAlotee').css('display', 'none');
        }
    });

    $('#SchemeType').on('change', function (e) {
        var valueSelected = this.value;
        FillSchemeName(valueSelected);
    });

    function FillSchemeName(SchemeType) {
        let dropdown = $('#SchemeName');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        if (SchemeType == 'Industrial') {
            dropdown.append($('<option></option>').attr('value', 'Industrial Shed Yojna').text('Industrial Shed Yojna'));
            dropdown.append($('<option></option>').attr('value', 'Industrial Yojna').text('Industrial Yojna'));
        }
        else if (SchemeType == 'Residential') {
            dropdown.append($('<option></option>').attr('value', 'Housing Vistahapan').text('Housing Vistahapan'));
            dropdown.append($('<option></option>').attr('value', 'New Gorakhpur Awasiya Yojna').text('New Gorakhpur Awasiya Yojna'));
            dropdown.append($('<option></option>').attr('value', 'Sahjanwa Awasiya Yojna').text('Sahjanwa Awasiya Yojna'));
        }
        else if (SchemeType == 'Institutional') {
            dropdown.append($('<option></option>').attr('value', 'Institutional Yojna').text('Institutional Yojna'));
        }
        else if (SchemeType == 'Commercial') {
            dropdown.append($('<option></option>').attr('value', 'Commercial Yojna').text('Commercial Yojna'));
        }
        else if (SchemeType == 'Transport Nagar') {
            dropdown.append($('<option></option>').attr('value', 'Transport Nagar').text('Transport Nagar'));
            dropdown.append($('<option></option>').attr('value', 'Shopping Complex Yojna').text('Shopping Complex Yojna'));
        }
        //$.ajax({
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    type: 'POST',
        //    data: '{clientId: "' + clientId + '" }',
        //    url: '/Masters/GetAllLRDetails',
        //    success: function (data) {
        //        var jsonData = JSON.parse(data);
        //        $.each(jsonData, function (key, entry) {
        //            dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.LRNumber));
        //        });
        //    },
        //    failure: function (response) {
        //        alert(response);
        //    },
        //    error: function (response) {
        //        alert(response.responseText);
        //    }
        //});
    }
    $('#SchemeName').on('change', function (e) {
        var valueSelected = this.value;
        FillSector(valueSelected);
    });

    function FillSector(SchemeName) {
        let dropdown = $('#SectorName');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        if (SchemeName == 'Industrial Shed Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 13').text('Sector 13'));
            dropdown.append($('<option></option>').attr('value', 'Sector 15').text('Sector 15'));
        }
        else if (SchemeName == 'Industrial Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 13').text('Sector 13'));
            dropdown.append($('<option></option>').attr('value', 'Sector 15').text('Sector 15'));
            dropdown.append($('<option></option>').attr('value', 'Sector 23').text('Sector 23'));
        }
        else if (SchemeName == 'Housing Vistahapan') {
            dropdown.append($('<option></option>').attr('value', 'Sector 22').text('Sector 22'));
        }
        else if (SchemeName == 'New Gorakhpur Awasiya Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 23').text('Sector 23'));
        }
        else if (SchemeName == 'Institutional Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 7').text('Sector 7'));
            dropdown.append($('<option></option>').attr('value', 'Sector 9').text('Sector 9'));
            dropdown.append($('<option></option>').attr('value', 'Sector 13').text('Sector 13'));
            dropdown.append($('<option></option>').attr('value', 'Sector 22').text('Sector 22'));
            dropdown.append($('<option></option>').attr('value', 'Sector 23').text('Sector 23'));
        }
        else if (SchemeName == 'Commercial Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 5').text('Sector 7'));
            dropdown.append($('<option></option>').attr('value', 'Sector 9').text('Sector 9'));
            dropdown.append($('<option></option>').attr('value', 'Sector 11').text('Sector 11'));
            dropdown.append($('<option></option>').attr('value', 'Sector 13').text('Sector 13'));
            dropdown.append($('<option></option>').attr('value', 'Sector 22').text('Sector 22'));
            dropdown.append($('<option></option>').attr('value', 'Sector 23').text('Sector 23'));
        }
        else if (SchemeName == 'Transport Nagar') {
            dropdown.append($('<option></option>').attr('value', 'Sector 22').text('Sector 22'));
            dropdown.append($('<option></option>').attr('value', 'Sector 23').text('Sector 23'));
        }
        else if (SchemeName == 'Shopping Complex Yojna') {
            dropdown.append($('<option></option>').attr('value', 'Sector 13').text('Sector 13'));
        }
        //$.ajax({
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    type: 'POST',
        //    data: '{clientId: "' + clientId + '" }',
        //    url: '/Masters/GetAllLRDetails',
        //    success: function (data) {
        //        var jsonData = JSON.parse(data);
        //        $.each(jsonData, function (key, entry) {
        //            dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.LRNumber));
        //        });
        //    },
        //    failure: function (response) {
        //        alert(response);
        //    },
        //    error: function (response) {
        //        alert(response.responseText);
        //    }
        //});
    }
   
    $('#btnPlotDetailSave').on('click', function (e) {
        var url = '/masters/SavePlotDetail';
        var inputData = {
            ApplicationFee : $('#ApplicationFee').val(),
            AppliedFor : $('#AppliedFor').val(),
            EarnestMoney : $('#EarnestMoneyDeposite').val(),
            EstimatedRate : $('#EstimatedRate').val(),
            GST : $('#GST').val(),
            IndustryOwnershipType : $('#IndustryOwnershipType').val(),
            NetAmount : $('#NetAmount').val(),
            PaymemtSchedule: $('#PaymemtSchedule').val(),
            PlotArea : $('#plotArea').val(),
            PlotRange : $('#PlotRange').val(),
            RelationshipStatus : $('#RelationshipStatus').val(),
            SchemeName : $('#SchemeName').val(),
            SchemeType : $('#SchemeType').val(),
            SectorName : $('#SectorName').val(),
            dob: $('#dob').val(),
            Name : $('#Name').val(),
            PermanentAddress : $('#PermanentAddress').val(),
            PresentAddress : $('#PresentAddress').val(),
            TotalAmount : $('#TotalAmount').val(),
            TotalInvestment : $('#TotalInvestment').val(),
            UnitName : $('#UnitName').val()
        };
        utility.ajax.helperWithData(url, inputData, function (data) {
            if (data == 'Data has been updated') {
                $('#progressbar li').removeClass('active');
                $('#ApplicantDetail').addClass('active');
                utility.alert.setAlert(utility.alert.alertType.success, 'Plot Detail has been Saved');
            }
        });
    });

    $('#Step2PreviousButton').on('click', function (e) {
        $('#progressbar li').removeClass('active');
        $('#PlotDetail').addClass('active');
    });

    $('#Step2NextButton').on('click', function (e) {
        var url = '/masters/SaveApplicantDetails';
        var IdentityProof = '';
        if ($('#Passport').prop('checked')) {
            IdentityProof = $('#Passport').val();
        }
        else if ($('#PAN').prop('checked')) {
            IdentityProof = $('#PAN').val();
        }
        else if ($('#voterIDCard').prop('checked')) {
            IdentityProof = $('#voterIDCard').val();
        }
        else if ($('#DrivingLiecence').prop('checked')) {
            IdentityProof = $('#DrivingLiecence').val();
        }
        else if ($('#AdhaarCard').prop('checked')) {
            IdentityProof = $('#AdhaarCard').val();
        }
        else if ($('#CompanyIDCard').prop('checked')) {
            IdentityProof = $('#CompanyIDCard').val();
        }

        var ResidentialProof = '';
        if ($('#ElectricBill').prop('checked')) {
            ResidentialProof = $('#ElectricBill').val();
        }
        else if ($('#ITR').prop('checked')) {
            ResidentialProof = $('#ITR').val();
        }
        else if ($('#TelephoneBill').prop('checked')) {
            ResidentialProof = $('#TelephoneBill').val();
        }
        else if ($('#BankPassbook').prop('checked')) {
            ResidentialProof = $('#BankPassbook').val();
        }
        else if ($('#Passport').prop('checked')) {
            ResidentialProof = $('#Passport').val();
        }
        else if ($('#VoterIDCard').prop('checked')) {
            ResidentialProof = $('#VoterIDCard').val();
        }
        else if ($('#HRBill').prop('checked')) {
            ResidentialProof = $('#HRBill').val();
        }
        else if ($('#DrivingLiecence').prop('checked')) {
            ResidentialProof = $('#DrivingLiecence').val();
        }

        var inputData = {
            FullName: $('#FullName').val(),
            FName: $('#FName').val(),
            MName: $('#MName').val(),
            SName: $('#SName').val(),
            DOB: $('#DOB').val(),
            Gender: $('#Gender').val(),
            Reservation: $('#Reservation').val(),
            Nationality: $('#Nationality').val(),
            AdhaarNo: $('#AdhaarNo').val(),
            PAN: $('#PAN').val(),
            MobileNo: $('#MobileNo').val(),
            Phone: $('#Phone').val(),
            Email: $('#Email').val(),
            Religion: $('#Religion').val(),
            SubCategory: $('#SubCategory').val(),
            CAddress: $('#CAddress').val(),
            PAddress: $('#PAddress').val(),
            IdentityProof: $('#IdentityProof').val(),
            ResidentialProof: $('#ResidentialProof').val(),
        };

        utility.ajax.helperWithData(url, inputData, function (data) {
            if (data == 'Data has been updated') {
                $('#progressbar li').removeClass('active');
                $('#ProjectDetail').addClass('active');
                utility.alert.setAlert(utility.alert.alertType.success, 'Applicant Detail has been Saved');
            }
        });
    });


    $('#Step3PreviousButton').on('click', function (e) {
        $('#progressbar li').removeClass('active');
        $('#ApplicantDetail').addClass('active');
    });

    $('#Step3NextButton').on('click', function (e) {
        var url = '/masters/SaveProjectDetails';
        var inputData = {
            ProposedIndustryType: $('#ProposedIndustryType').val(),
            ProjectEstimatedCost: $('#ProjectEstimatedCost').val(),
            ProposedCoveredArea: $('#CoveredArea').val(),
            ProposedOpenArea: $('#OpenArea').val(),
            PurpuseOpenArea: $('#Purposeforopenarea').val(),
            ProposedInvestmentLand: $('#Investmentland').val(),
            ProposedInvestmentBuilding: $('#InvestmentBuilding').val(),
            ProposedInvestmentPlant: $('#InvestmentPlant').val(),
            FumesNatureQuantity: $('#processofmanufacture').val(),
            LiquidQuantity: $('#LiquidQuantity').val(),
            LiquidChemicalComposition: $('#LiquidChemicalComposition').val(),
            SolidQuantity: $('#SolidQuantity').val(),
            SolidChemicalComposition: $('#SolidChemicalComposition').val(),
            GasQuantity: $('#GasQuantity').val(),
            GasChemicalComposition: $('#GasChemicalComposition').val(),
            //EffluentTreatmentMeasures: $('#GasChemicalComposition').val(),
            PowerRequirement: $('#PowerRequirement').val(),
            FirstYearNoOfTelephone: $('#FirstYearTelephonicConnection').val(),
            FirstYearNoOfFax: $('#FirstYearFaxConnection').val(),
            UltimateNoOfTelephone: $('#UltimateRequirementTelephonicConnection').val(),
            UltimateNoOfFax: $('#UltimateRequirementFaxConnection').val(),
            Skilled: $('#Skilled').val(),
            UnSkilled: $('#Unskilled').val(),
        };

        utility.ajax.helperWithData(url, inputData, function (data) {
            if (data == 'Data has been updated') {
                $('#progressbar li').removeClass('active');
                $('#BankDetail').addClass('active');
                utility.alert.setAlert(utility.alert.alertType.success, 'Project Detail has been Saved');
            }
        });
    });

    $('#Step4PreviousButton').on('click', function (e) {
        $('#progressbar li').removeClass('active');
        $('#ProjectDetail').addClass('active');
    });

    $('#Step4NextButton').on('click', function (e) {
        var url = '/masters/SaveBankDetail';
        var inputData = {
            BankAccountName: $('#BankAccountName').val(),
            BankAccountNo: $('#BankAccountNo').val(),
            BankName: $('#BankName').val(),
            BranchName: $('#BranchName').val(),
            BranchAddress: $('#BranchAddress').val(),
            IFSCCode: $('#IFSCCode').val(),
        };

        utility.ajax.helperWithData(url, inputData, function (data) {
            if (data == 'Data has been updated') {
                $('#progressbar li').removeClass('active');
                $('#AttachDocument').addClass('active');
                utility.alert.setAlert(utility.alert.alertType.success, 'Bank Detail has been Saved');
            }
        });
    });

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

});

