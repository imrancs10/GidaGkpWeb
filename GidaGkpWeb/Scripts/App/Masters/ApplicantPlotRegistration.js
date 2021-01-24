/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />
'use strict';


$(document).ready(function () {
    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;


    var applicationId = getUrlParameter('applicationId');
    if (applicationId != null && applicationId != undefined && applicationId > 0) {
        getPlotRegistrationDetail(applicationId);
    }
    else {
        FillSchemeType();
        FillAppliedFor();
        FillPlotRange();
        FillRelationshipStatus();
        FillIndustryOwnershipType();
        FillPaymemtSchedule();
    }

    function getPlotRegistrationDetail(applicationId) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{applicationId: ' + applicationId + '}',
            url: '/Applicant/GetPlotRegistrationDetail',
            success: function (data) {
                if (data != null && data != undefined) {
                    FillAppliedFor(data.AppliedFor);
                    FillSchemeType(data.SchemeType)
                    FillSchemeName(data.SchemeType, data.SchemeName);
                    FillSector(data.SchemeName, data.SectorName);

                    FillPlotRange(data.PlotRange);
                    FillRelationshipStatus(data.RelationshipStatus);
                    FillIndustryOwnershipType(data.IndustryOwnership);
                    FillPaymemtSchedule(data.PaymentSchedule);

                    $('#plotArea').val(data.PlotArea);
                    $('#EstimatedRate').val(data.EstimatedRate);
                    $('#TotalInvestment').val(data.TotalInvestment);
                    $('#ApplicationFee').val(data.ApplicationFee);
                    $('#EarnestMoneyDeposite').val(data.EarnestMoney);
                    $('#GST').val(data.GST);
                    $('#NetAmount').val(data.NetAmount);
                    $('#TotalAmount').val(data.TotalAmount);
                    $('#UnitName').val(data.UnitName);
                    $('#Name').val(data.SignatryName);
                    $('#PresentAddress').val(data.SignatryPresentAddress);
                    var milli = data.SignatryDateOfBirth.replace(/\/Date\((-?\d+)\)\//, '$1');
                    var now = new Date(parseInt(milli));

                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);

                    var today = now.getFullYear() + "-" + (month) + "-" + (day);

                    $('#dob').val(today);
                    $('#PermanentAddress').val(data.SignatryPermanentAddress);
                    $('#btnStep1Skip').removeClass('hidden');
                    $('#spanApplicationNumber').html(data.ApplicationNumber);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function getApplicantPersonalDetail() {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '',
            url: '/Applicant/GetApplicantPersonalDetail',
            success: function (data) {
                if (data != null && data != undefined) {
                    //IdentiyProof
                    if (data.IdentiyProof =='Passport') {
                        $('#IDPassport').prop('checked', true);
                    }
                    else if (data.IdentiyProof == 'PAN') {
                        $('#IDPAN').prop('checked', true);
                    }
                    else if (data.IdentiyProof == 'voterIDCard') {
                        $('#IDvoterIDCard').prop('checked', true);
                    }
                    else if (data.IdentiyProof == 'DrivingLiecence') {
                        $('#IDDrivingLiecence').prop('checked', true);
                    }
                    else if (data.IdentiyProof == 'AdhaarCard') {
                        $('#IDAdhaarCard').prop('checked', true);
                    }
                    else if (data.IdentiyProof == 'CompanyIDCard') {
                        $('#IDCompanyIDCard').prop('checked', true);
                    }

                    //ResidentialProof
                    if (data.ResidentialProof == 'ElectricBill') {
                        $('#RPElectricBill').prop('checked', true);
                    }
                    else if (data.ResidentialProof == 'ITR') {
                        $('#RPITR').prop('checked', true);
                    }
                    else if (data.ResidentialProof == 'TelephoneBill') {
                        $('#RPTelephoneBill').prop('checked', true);
                    }

                    else if (data.ResidentialProof == 'BankPassbook') {
                        $('#RPBankPassbook').prop('checked', true);
                    }
                    else if (data.ResidentialProof == 'Passport') {
                        $('#RPPassport').prop('checked', true);
                    }
                    else if (data.ResidentialProof == 'VoterIDCard') {
                        $('#RPVoterIDCard').prop('checked', true);
                    }

                    else if (data.ResidentialProof == 'HRBill') {
                        $('#RPHRBill').prop('checked', true);
                    }
                    else if (data.ResidentialProof == 'DrivingLiecence') {
                        $('#RPDrivingLiecence').prop('checked', true);
                    }

                    $('#FullName').val(data.FullApplicantName);
                    $('#FName').val(data.FName);
                    $('#MName').val(data.MName);
                    $('#SName').val(data.SName);
                    $('#Gender').val(data.Gender);
                    $('#Reservation').val(data.Category);
                    $('#Nationality').val(data.Nationality);
                    $('#AdhaarNo').val(data.AdhaarNumber);
                    $('#PAN').val(data.PAN);
                    $('#MobileNo').val(data.Mobile);
                    $('#Phone').val(data.Phone);
                    $('#Email').val(data.EmailId);
                    $('#Religion').val(data.Religion);
                    $('#SubCategory').val(data.SubCategory);
                    $('#CAddress').val(data.CAddress);
                    $('#PAddress').val(data.PAddress);

                    var milli = data.ApplicantDOB.replace(/\/Date\((-?\d+)\)\//, '$1');
                    var now = new Date(parseInt(milli));

                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);

                    var today = now.getFullYear() + "-" + (month) + "-" + (day);
                    $('#DOB').val(today);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillSchemeType(selectedSchemeTypeId = null) {
        let dropdown = $('#SchemeType');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "SchemeType" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedSchemeTypeId != null) {
                    dropdown.val(selectedSchemeTypeId);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    $('#SchemeType').on('change', function (e) {
        var valueSelected = this.value;
        FillSchemeName(valueSelected);
    });

    function FillAppliedFor(selectedAppliedForID = null) {
        let dropdown = $('#AppliedFor');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "AppliedFor" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedAppliedForID != null) {
                    dropdown.val(selectedAppliedForID);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillSchemeName(SchemeTypeId, selectedSchemeNameId = null) {
        let dropdown = $('#SchemeName');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: "' + SchemeTypeId + '",lookupType: "SchemeName" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedSchemeNameId != null) {
                    dropdown.val(selectedSchemeNameId);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
    $('#SchemeName').on('change', function (e) {
        var valueSelected = this.value;
        FillSector(valueSelected);
    });

    $('#SectorName').on('change', function (e) {
        $('#SectorDescription').val($("#SectorName option:selected").text());
    });

    function FillSector(SchemeNameId, selectedSectorId = null) {
        let dropdown = $('#SectorName');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: "' + SchemeNameId + '",lookupType: "SectorName" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedSectorId != null) {
                    dropdown.val(selectedSectorId);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillPlotRange(selectedPlotRange = null) {
        let dropdown = $('#PlotRange');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "PlotRange" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedPlotRange != null) {
                    dropdown.val(selectedPlotRange);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillPaymemtSchedule(selectedPaymemtSchedule = null) {
        let dropdown = $('#PaymemtSchedule');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "PaymemtSchedule" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedPaymemtSchedule != null) {
                    dropdown.val(selectedPaymemtSchedule);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillIndustryOwnershipType(selectedIndustryOwnershipType = null) {
        let dropdown = $('#IndustryOwnershipType');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "IndustryOwnershipType" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedIndustryOwnershipType != null) {
                    dropdown.val(selectedIndustryOwnershipType);
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function FillRelationshipStatus(selectedRelationshipStatus = null) {
        let dropdown = $('#RelationshipStatus');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "RelationshipStatus" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedRelationshipStatus != null) {
                    dropdown.val(selectedRelationshipStatus)
                }
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    $('#btnPlotDetailSave').on('click', function (e) {
        if ($('#ApplicationFee').val() != '' && $('#AppliedFor').val() != '' && $('#EarnestMoneyDeposite').val() != '') {
            var url = '/Applicant/SavePlotDetail';
            var inputData = {
                ApplicationFee: $('#ApplicationFee').val(),
                AppliedFor: $('#AppliedFor').val(),
                EarnestMoney: $('#EarnestMoneyDeposite').val(),
                EstimatedRate: $('#EstimatedRate').val(),
                GST: $('#GST').val(),
                IndustryOwnershipType: $('#IndustryOwnershipType').val(),
                NetAmount: $('#NetAmount').val(),
                PaymemtSchedule: $('#PaymemtSchedule').val(),
                PlotArea: $('#plotArea').val(),
                PlotRange: $('#PlotRange').val(),
                RelationshipStatus: $('#RelationshipStatus').val(),
                SchemeName: $('#SchemeName').val(),
                SchemeType: $('#SchemeType').val(),
                SectorName: $('#SectorName').val(),
                dob: $('#dob').val(),
                Name: $('#Name').val(),
                PermanentAddress: $('#PermanentAddress').val(),
                PresentAddress: $('#PresentAddress').val(),
                TotalAmount: $('#TotalAmount').val(),
                TotalInvestment: $('#TotalInvestment').val(),
                UnitName: $('#UnitName').val(),
                SectorDescription: $('#SectorDescription').val()
            };
            utility.ajax.helperWithData(url, inputData, function (data) {
                if (data != 'Error') {
                    //$('#progressbar li').removeClass('active');
                    //$('#ApplicantDetail').addClass('active');
                    NextStep($('#btnPlotDetailSave'));
                    getApplicantPersonalDetail();
                    $('#spanApplicationNumber').html(data);
                    $('#divApplicationNumber').css('display', 'block');

                    utility.alert.setAlert(utility.alert.alertType.success, 'Plot Detail has been Saved, Your Application Number is ' + data);
                }
            });
        }
        else {
            utility.alert.setAlert(utility.alert.alertType.error, 'Please fill required input.');
        }

    });

    $('#Step2PreviousButton').on('click', function (e) {
        //$('#progressbar li').removeClass('active');
        //$('#PlotDetail').addClass('active');
        PreviousStep($('#Step2PreviousButton'));
    });

    $('#Step2NextButton').on('click', function (e) {
        if ($('#FullName').val() != '' && $('#FName').val() != '' && $('#MName').val() != '') {
            var url = '/Applicant/SaveApplicantDetails';
            var IdentityProof = '';
            if ($('#IDPassport').prop('checked')) {
                IdentityProof = $('#Passport').val();
            }
            else if ($('#IDPAN').prop('checked')) {
                IdentityProof = $('#PAN').val();
            }
            else if ($('#IDvoterIDCard').prop('checked')) {
                IdentityProof = $('#voterIDCard').val();
            }
            else if ($('#IDDrivingLiecence').prop('checked')) {
                IdentityProof = $('#DrivingLiecence').val();
            }
            else if ($('#IDAdhaarCard').prop('checked')) {
                IdentityProof = $('#AdhaarCard').val();
            }
            else if ($('#IDCompanyIDCard').prop('checked')) {
                IdentityProof = $('#CompanyIDCard').val();
            }

            var ResidentialProof = '';
            if ($('#RPElectricBill').prop('checked')) {
                ResidentialProof = $('#ElectricBill').val();
            }
            else if ($('#RPITR').prop('checked')) {
                ResidentialProof = $('#ITR').val();
            }
            else if ($('#RPTelephoneBill').prop('checked')) {
                ResidentialProof = $('#TelephoneBill').val();
            }
            else if ($('#RPBankPassbook').prop('checked')) {
                ResidentialProof = $('#BankPassbook').val();
            }
            else if ($('#RPPassport').prop('checked')) {
                ResidentialProof = $('#Passport').val();
            }
            else if ($('#RPVoterIDCard').prop('checked')) {
                ResidentialProof = $('#VoterIDCard').val();
            }
            else if ($('#RPHRBill').prop('checked')) {
                ResidentialProof = $('#HRBill').val();
            }
            else if ($('#RPDrivingLiecence').prop('checked')) {
                ResidentialProof = $('#DrivingLiecence').val();
            }

            var inputData = {
                FullName: $('#FullName').val(),
                FName: $('#FName').val(),
                MName: $('#MName').val(),
                SName: $('#SName').val(),
                DOB: $('#DOB').val(),
                Gender: $('#Gender').val(),
                Category: $('#Reservation').val(),
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
                if (data == 'Data has been saved') {
                    //$('#progressbar li').removeClass('active');
                    //$('#ProjectDetail').addClass('active');
                    NextStep($('#Step2NextButton'));
                    utility.alert.setAlert(utility.alert.alertType.success, 'Applicant Detail has been Saved');
                }
            });
        }
        else {
            utility.alert.setAlert(utility.alert.alertType.error, 'Please fill required input.');
        }

    });


    $('#Step3PreviousButton').on('click', function (e) {
        //$('#progressbar li').removeClass('active');
        //$('#ApplicantDetail').addClass('active');
        PreviousStep($('#Step3PreviousButton'));
    });

    $('#Step3NextButton').on('click', function (e) {
        if ($('#ProposedIndustryType').val() != '' && $('#ProjectEstimatedCost').val() != '' && $('#CoveredArea').val() != '') {
            var url = '/Applicant/SaveProjectDetails';
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
                if (data == 'Data has been saved') {
                    //$('#progressbar li').removeClass('active');
                    //$('#BankDetail').addClass('active');
                    utility.alert.setAlert(utility.alert.alertType.success, 'Project Detail has been Saved');
                    NextStep($('#Step3NextButton'));
                }
            });
        }
        else {
            utility.alert.setAlert(utility.alert.alertType.error, 'Please fill required input.');
        }

    });

    $('#Step4PreviousButton').on('click', function (e) {
        //$('#progressbar li').removeClass('active');
        //$('#ProjectDetail').addClass('active');
        PreviousStep($('#Step4PreviousButton'));
    });

    $('#Step4NextButton').on('click', function (e) {
        if ($('#BankAccountName').val() != '' && $('#BankAccountNo').val() != '' && $('#BankName').val() != '') {
            var url = '/Applicant/SaveBankDetail';
            var inputData = {
                BankAccountName: $('#BankAccountName').val(),
                BankAccountNo: $('#BankAccountNo').val(),
                BankName: $('#BankName').val(),
                BranchName: $('#BranchName').val(),
                BranchAddress: $('#BranchAddress').val(),
                IFSCCode: $('#IFSCCode').val(),
            };

            utility.ajax.helperWithData(url, inputData, function (data) {
                if (data == 'Data has been saved') {
                    //$('#progressbar li').removeClass('active');
                    //$('#AttachDocument').addClass('active');
                    utility.alert.setAlert(utility.alert.alertType.success, 'Bank Detail has been Saved');
                    NextStep($('#Step4NextButton'));
                }
            });
        }
        else {
            utility.alert.setAlert(utility.alert.alertType.error, 'Please fill required input.');
        }

    });

    $('#Step5PreviousButton').on('click', function (e) {
        //$('#progressbar li').removeClass('active');
        //$('#BankDetail').addClass('active');
        PreviousStep($('#Step5PreviousButton'));
    });

    $("#PlotRange").change(function () {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: "' + $(this).val() + '",lookupType: "EstimatedRate" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $('#EstimatedRate').val(data[0].LookupName);
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });


    $("#plotArea").change(function () {
        var plotRangeSelected = $("#PlotRange option:selected").text();
        if (plotRangeSelected.indexOf('Above') > -1) {
            var rangeArray = plotRangeSelected.split('-');
            if (parseInt($(this).val()) < parseInt(rangeArray[0])) {
                $(this).val('')
                utility.alert.setAlert(utility.alert.alertType.info, 'Plot Area must in selected plot range');
            }
        }
        else {
            var rangeArray = plotRangeSelected.split('-');
            if (parseInt($(this).val()) < parseInt(rangeArray[0]) || parseInt($(this).val()) > parseInt(rangeArray[1])) {
                $(this).val('')
                utility.alert.setAlert(utility.alert.alertType.info, 'Plot Area must in selected plot range');
            }
        }
        if ($(this).val() != '' && $('#EstimatedRate').val() != '') {
            $('#TotalInvestment').val($(this).val() * $('#EstimatedRate').val());
        }
        var auxValue = (parseInt($(this).val()) + 1000).toString().slice(1, 4);
        var EMD = "";
        if (auxValue == "000") {
            EMD = (parseInt($(this).val())).toString().slice(0, -3) + "0000";
        }
        else {
            EMD = (parseInt($(this).val()) + 1000).toString().slice(0, -3) + "0000";
        }
        $('#EarnestMoneyDeposite').val(EMD);
        if ($('#EarnestMoneyDeposite').val() != '') {
            $('#NetAmount').val(parseInt($('#EarnestMoneyDeposite').val()) + parseInt($('#TotalAmount').val()));
        }
    });


    $('#btnStep1Skip').click(function () {
        NextStep($('#btnStep1Skip'));
        getApplicantPersonalDetail();
    });

    $('#btnStep2Skip').click(function () {
        NextStep($('#btnStep2Skip'));
    });

    function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    }

    function NextStep(nextButton) {

        current_fs = $(nextButton).parent().parent().parent();
        next_fs = $(nextButton).parent().parent().parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    }

    function PreviousStep(previousButton) {

        current_fs = $(previousButton).parent().parent().parent();
        previous_fs = $(previousButton).parent().parent().parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    }

});

