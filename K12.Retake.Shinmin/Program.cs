using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;
using FISCA.Permission;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;
using System.Windows.Forms;
using Campus.DocumentValidator;
using System.ComponentModel;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 新民客製重補修系統
    /// </summary>
    public class Program
    {
        static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
        [MainMethod()]
        public static void Main()
        {
            // 更新 UDS UDT 方式            
            if (!FISCA.RTContext.IsDiagMode)
                FISCA.ServerModule.AutoManaged("http://module.ischool.com.tw/module/137/Retake_Shinmin_dep/udm.xml");

            #region 自訂驗證規則
            FactoryProvider.FieldFactory.Add(new FieldValidatorFactory());
            FactoryProvider.RowFactory.Add(new RowValidatorFactory());
            #endregion
            _bgLLoadUDT.DoWork += new DoWorkEventHandler(_bgLLoadUDT_DoWork);
            _bgLLoadUDT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLLoadUDT_RunWorkerCompleted);
            _bgLLoadUDT.RunWorkerAsync();
            MotherForm.AddPanel(RetakeAdmin.Instance);
            
            // Add ListView
            RetakeAdmin.Instance.AddView(new RetakeViewTree());

            RetakeAdmin.Instance.SelectedSourceChanged += delegate
            {
                FISCA.Presentation.MotherForm.SetStatusBarMessage("選擇「"+RetakeAdmin.Instance.SelectedSource.Count+"」個課程");
            };

            RibbonBarItem item08 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["編輯"];
            item08["刪除課程"].Image = Properties.Resources.刪除課程;
            item08["刪除課程"].Size = RibbonBarButton.MenuButtonSize.Large;
            item08["刪除課程"].Enable = UserAcl.Current["K12.Retake.Shinmin.DeleteCourse"].Executable;
            item08["刪除課程"].Click += delegate
            {
                DeleteCourse();
            };

            RibbonBarItem item08A = Shinmin.RetakeAdmin.Instance.RibbonBarItems["編輯"];
            item08A["新增課程"].Image = Properties.Resources.新增課程;
            item08A["新增課程"].Size = RibbonBarButton.MenuButtonSize.Large;
            item08A["新增課程"].Enable = UserAcl.Current["K12.Retake.Shinmin.AddCourse"].Executable;
            item08A["新增課程"].Click += delegate
            {
                Form.AddCourse ac = new Form.AddCourse();
                //ac.ShowDialog();
            };


            RibbonBarItem item01 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item01["建議重補修名單"].Image = Properties.Resources.建議重補修名單_filter_data_add_64;
            item01["建議重補修名單"].Size = RibbonBarButton.MenuButtonSize.Large;
            item01["建議重補修名單"].Enable = UserAcl.Current["K12.Retake.Shinmin.SuggestListForm"].Executable;
            item01["建議重補修名單"].Click += delegate
            {
                Form.SuggestListForm slf = new Form.SuggestListForm();
                slf.ShowDialog();
            };


            RibbonBarItem item02 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item02["科目管理"].Image = Properties.Resources.重補修科目管理_project_64;
            item02["科目管理"].Size = RibbonBarButton.MenuButtonSize.Large;
            item02["科目管理"].Enable = UserAcl.Current["K12.Retake.Shinmin.SubjectListForm"].Executable;
            item02["科目管理"].Click += delegate
            {
                Form.SubjectListForm slf = new Form.SubjectListForm();
                if (slf._isShowForm)
                    slf.ShowDialog();
                else
                    slf.Close();
            };


            RibbonBarItem item03 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item03["課表管理"].Image = Properties.Resources.重補修課表管理_schedule_write_64;
            item03["課表管理"].Size = RibbonBarButton.MenuButtonSize.Large;
            item03["課表管理"].Enable = UserAcl.Current["K12.Retake.Shinmin.CourseTimetableForm"].Executable;
            item03["課表管理"].Click += delegate
            {
                Form.CourseTimetableForm ctf = new Form.CourseTimetableForm();
                ctf.ShowDialog();
            };

            RibbonBarItem item05 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item05["選課開放時間"].Enable = UserAcl.Current["K12.Retake.Shinmin.RetakeJoinForm"].Executable;
            item05["選課開放時間"].Size = RibbonBarButton.MenuButtonSize.Medium;
            item05["選課開放時間"].Image = Properties.Resources.time_frame_refresh_128;
            item05["選課開放時間"].Click += delegate
            {
                Form.RetakeJoinForm ccif = new Form.RetakeJoinForm();
                ccif.ShowDialog();
            };

            RibbonBarItem item04 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item04["開課"].Image = Properties.Resources.開課;
            item04["開課"].Size = RibbonBarButton.MenuButtonSize.Large;
            item04["開課"].Enable = UserAcl.Current["K12.Retake.Shinmin.CourseTimetableForm"].Executable;
            item04["開課"].Click += delegate
            {
                Form.CreateCourseInfoForm ccif = new Form.CreateCourseInfoForm();
                ccif.ShowDialog();
            };

            RibbonBarItem item11 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["前置作業"];
            item11["時間表設定"].Enable = UserAcl.Current["K12.Retake.Shinmin.ReSetSubjectDate"].Executable;
            item11["時間表設定"].Size = RibbonBarButton.MenuButtonSize.Medium;
            item11["時間表設定"].Image = Properties.Resources.lesson_planning_clock_64;
            item11["時間表設定"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Form.ReSetSubjectDate ccif = new Form.ReSetSubjectDate();
                    ccif.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇課程!!");
                }
            };


           

            RibbonBarItem item09 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            item09["匯入"].Image = Properties.Resources.Import_Image;
            item09["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;
            item09["匯入"]["匯入課程"].Enable = UserAcl.Current["K12.Retake.Shinmin.ImportCourse"].Executable;
            item09["匯入"]["匯入課程"].Click += delegate
            {
                new ImportCourseExtension().Execute();
                RetakeEvents.RaiseAssnChanged();
            };

            RibbonBarItem item10 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            item10["匯入"]["匯入修課學生"].Enable = UserAcl.Current["K12.Retake.Shinmin.ImportSCAttend"].Executable;
            item10["匯入"]["匯入修課學生"].Click += delegate
            {
                new ImportSCAttend().Execute();
                RetakeEvents.RaiseAssnChanged();
            };

            RibbonBarItem itemRpt01 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemRpt01["報表"]["課程點名單"].Enable = UserAcl.Current["K12.Retake.Shinmin.Report.CourseStudentSCReport"].Executable;
            itemRpt01["報表"].Image = Properties.Resources.Report;
            itemRpt01["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemRpt01["報表"]["課程點名單"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Report.CourseStudentSCReportForm cssf = new Report.CourseStudentSCReportForm(RetakeAdmin.Instance.SelectedSource);
                    cssf.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇課程!");
                }
            };

            RibbonBarItem itemRpt02 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemRpt02["報表"]["課程缺曠名單"].Enable = UserAcl.Current["K12.Retake.Shinmin.Report.CourseStudentAttendReport"].Executable;
            itemRpt02["報表"].Image = Properties.Resources.Report;
            itemRpt02["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemRpt02["報表"]["課程缺曠名單"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Report.CourseStudentAttendReportForm csscf = new Report.CourseStudentAttendReportForm(RetakeAdmin.Instance.SelectedSource);
                    csscf.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇課程!");
                }
            };

            RibbonBarItem itemRpt03 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemRpt03["報表"]["缺曠通知單"].Enable = UserAcl.Current["K12.Retake.Shinmin.Report.StudentAttendanceReportForm"].Executable;
            itemRpt03["報表"].Image = Properties.Resources.Report;
            itemRpt03["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemRpt03["報表"]["缺曠通知單"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Report.StudentAttendanceReportForm sarf = new Report.StudentAttendanceReportForm(RetakeAdmin.Instance.SelectedSource);
                    sarf.ShowDialog();

                }
                else
                {
                    MsgBox.Show("請選擇課程!");
                }
            };

            RibbonBarItem itemRpt04 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemRpt04["報表"]["及格人數"].Enable = UserAcl.Current["K12.Retake.Shinmin.Report.StudentPassReport"].Executable;
            itemRpt04["報表"].Image = Properties.Resources.Report;
            //itemRpt04["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemRpt04["報表"]["及格人數"].Click += delegate
            {
                Report.StudentPassReport spr = new Report.StudentPassReport();
                spr.Main();
            };

            RibbonBarItem itemRpt05 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemRpt05["報表"]["課程缺曠統計表"].Enable = UserAcl.Current["K12.Retake.Shinmin.Report.CourseAttendanceRpt"].Executable;
            itemRpt05["報表"].Image = Properties.Resources.Report;
            itemRpt05["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemRpt05["報表"]["課程缺曠統計表"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Report.CourseAttendanceRpt car = new Report.CourseAttendanceRpt(RetakeAdmin.Instance.SelectedSource);
                    car.Run();

                }
                else
                {
                    MsgBox.Show("請選擇課程!");
                }
            };


            RibbonBarItem Results = RetakeAdmin.Instance.RibbonBarItems["成績"];
            Results["評量設定"].Size = RibbonBarButton.MenuButtonSize.Medium;
            Results["評量設定"].Image = Properties.Resources.barchart_64;
            Results["評量設定"].Enable = UserAcl.Current["K12.Retake.Shinmin.GradingProjectConfigForm"].Executable;
            Results["評量設定"].Click += delegate
            {
                Form.GradingProjectConfigForm gpcf = new Form.GradingProjectConfigForm();
                gpcf.ShowDialog();
            };

            RibbonBarItem item06 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["成績"];
            item06["成績輸入區間"].Enable = UserAcl.Current["K12.Retake.Shinmin.ResultsInputForm"].Executable;
            item06["成績輸入區間"].Size = RibbonBarButton.MenuButtonSize.Medium;
            item06["成績輸入區間"].Image = Properties.Resources.time_frame_refresh_128;
            item06["成績輸入區間"].Click += delegate
            {
                Form.ResultsInputForm ccif = new Form.ResultsInputForm();
                ccif.ShowDialog();
            };

            Results["成績輸入"].Size = RibbonBarButton.MenuButtonSize.Medium;
            Results["成績輸入"].Image = Properties.Resources.marker_fav_64;
            Results["成績輸入"].Enable = UserAcl.Current["K12.Retake.Shinmin.RetakeResultsInputForm"].Executable;
            Results["成績輸入"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Form.RetakeResultsInputForm rrif = new Form.RetakeResultsInputForm(RetakeAdmin.Instance.SelectedSource);
                    rrif.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇課程!");
                }
            };

            Results["成績結算"].Size = RibbonBarButton.MenuButtonSize.Medium;
            Results["成績結算"].Image = Properties.Resources.brand_write_64;
            Results["成績結算"].Enable = UserAcl.Current["K12.Retake.Shinmin.ClearingForm"].Executable;
            Results["成績結算"].Click += delegate
            {
                Form.ClearingForm cf = new Form.ClearingForm();                
                cf.ShowDialog();
            };


            RibbonBarItem item07 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["缺曠"];
            item07["缺曠登錄"].Enable = UserAcl.Current["K12.Retake.Shinmin.AttendanceForm"].Executable;
            item07["缺曠登錄"].Size = RibbonBarButton.MenuButtonSize.Medium;
            item07["缺曠登錄"].Image = Properties.Resources.desk_64;
            item07["缺曠登錄"].Click += delegate
            {
                Form.AttendanceForm ccif = new Form.AttendanceForm();
                ccif.ShowDialog();
            };

            RibbonBarItem item07a = Shinmin.RetakeAdmin.Instance.RibbonBarItems["缺曠"];
            item07a["扣考查詢"].Enable = UserAcl.Current["K12.Retake.Shinmin.StudentNotExamSearchForm"].Executable;
            item07a["扣考查詢"].Size = RibbonBarButton.MenuButtonSize.Medium;
            item07a["扣考查詢"].Image = Properties.Resources.desk_64;
            item07a["扣考查詢"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    Form.StudentNotExamSearchForm snesf = new Form.StudentNotExamSearchForm(RetakeAdmin.Instance.SelectedSource);
                    snesf.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇課程!!");
                    return;
                }

            };


            RibbonBarItem itemEp01 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemEp01["匯出"].Image = Properties.Resources.匯出;
            itemEp01["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemEp01["匯出"]["課程學期成績匯入檔"].Enable = UserAcl.Current["K12.Retake.Shinmin.ExportCourseScore"].Executable;
            itemEp01["匯出"]["課程學期成績匯入檔"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    ImportExport.ExportCourseScore ecs = new ImportExport.ExportCourseScore(RetakeAdmin.Instance.SelectedSource);
                }
                else
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
            };

            RibbonBarItem itemEp02 = Shinmin.RetakeAdmin.Instance.RibbonBarItems["資料統計"];
            itemEp02["匯出"].Image = Properties.Resources.匯出;
            itemEp02["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
            itemEp02["匯出"]["學生選課清單"].Enable = UserAcl.Current["K12.Retake.Shinmin.ExportStudentCourseSelect"].Executable;
            itemEp02["匯出"]["學生選課清單"].Click += delegate
            {
                if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                {
                    ImportExport.ExportStudentCourseSelect escs = new ImportExport.ExportStudentCourseSelect(RetakeAdmin.Instance.SelectedSource);
                }
                else
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
            };

            //其它
            MenuButton MenuItem09 = Shinmin.RetakeAdmin.Instance.ListPaneContexMenu["刪除課程"];
            MenuItem09.Enable = UserAcl.Current["K12.Retake.Shinmin.DeleteCourse"].Executable;
            MenuItem09.Click += delegate
            {
                DeleteCourse();
            };

            // 報表 重補修缺課(含扣考)通知單( 在學生>
            var studItemRpt01 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["新民重補修報表"]["重補修缺課(含扣考)通知單"];
            studItemRpt01.Enable = UserAcl.Current["K12.Retake.Shinmin.Report.StudentCourseAttendanceRptForm"].Executable;
            studItemRpt01.Image = Properties.Resources.Report;            
            studItemRpt01.Click += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    Report.StudentCourseAttendanceRptForm scarf = new Report.StudentCourseAttendanceRptForm(K12.Presentation.NLDPanels.Student.SelectedSource);
                    scarf.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇學生!");
                }
            };



            // 建議重補修名單
            Catalog catalog01 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("K12.Retake.Shinmin.SuggestListForm", "建議重補修名單"));

            // 重補修科目管理
            Catalog catalog02 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog02.Add(new RibbonFeature("K12.Retake.Shinmin.SubjectListForm", "科目管理"));

            // 重補修課表管理
            Catalog catalog03 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog03.Add(new RibbonFeature("K12.Retake.Shinmin.CourseTimetableForm", "課表管理"));

            // 重補修開課
            Catalog catalog04 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog04.Add(new RibbonFeature("K12.Retake.Shinmin.CreateCourseInfoForm", "開課"));

            //選課開放時間
            Catalog catalog05 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog05.Add(new RibbonFeature("K12.Retake.Shinmin.RetakeJoinForm", "選課開放時間"));

            //成績輸入區間
            Catalog catalog06 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog06.Add(new RibbonFeature("K12.Retake.Shinmin.ResultsInputForm", "成績輸入區間"));

            //課程缺曠登錄
            Catalog catalog07 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog07.Add(new RibbonFeature("K12.Retake.Shinmin.AttendanceForm", "缺曠登錄"));

            //課程扣考查詢
            Catalog catalog07a = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog07a.Add(new RibbonFeature("K12.Retake.Shinmin.StudentNotExamSearchForm", "扣考查詢"));


            // 刪除課程
            Catalog catalog08 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog08.Add(new RibbonFeature("K12.Retake.Shinmin.DeleteCourse", "刪除課程"));

            // 新增課程
            Catalog catalog08A = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog08A.Add(new RibbonFeature("K12.Retake.Shinmin.AddCourse", "新增課程"));


            //匯入課程
            Catalog catalog09 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog09.Add(new RibbonFeature("K12.Retake.Shinmin.ImportCourse", "匯入課程"));

            //匯出課程成績
            Catalog catalogSc01 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalogSc01.Add(new RibbonFeature("K12.Retake.Shinmin.ExportCourseScore", "課程學期成績匯入檔"));

            //匯出學生選課清單
            Catalog catalogSc02 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalogSc02.Add(new RibbonFeature("K12.Retake.Shinmin.ExportStudentCourseSelect", "學生選課清單"));

            //匯入學生修課
            Catalog catalog20 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog20.Add(new RibbonFeature("K12.Retake.Shinmin.ImportSCAttend", "匯入學生修課"));

            //時間表設定
            Catalog catalog21 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog21.Add(new RibbonFeature("K12.Retake.Shinmin.ReSetSubjectDate", "時間表設定"));

            // 報表 課程點名單
            Catalog catalog22= RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog22.Add(new RibbonFeature("K12.Retake.Shinmin.Report.CourseStudentSCReport", "課程點名單"));

            // 報表 課程缺曠名單
            Catalog catalog23 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog23.Add(new RibbonFeature("K12.Retake.Shinmin.Report.CourseStudentAttendReport", "課程缺曠名單"));

            // 報表 缺曠通知單
            Catalog catalog24 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog24.Add(new RibbonFeature("K12.Retake.Shinmin.Report.StudentAttendanceReportForm", "缺曠通知單"));

            // 成績 評量設定
            Catalog catalog25 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog25.Add(new RibbonFeature("K12.Retake.Shinmin.GradingProjectConfigForm", "評量設定"));

            // 成績 成績輸入
            Catalog catalog26 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog26.Add(new RibbonFeature("K12.Retake.Shinmin.RetakeResultsInputForm", "成績輸入"));

            // 成績 學期結算
            Catalog catalog27 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog27.Add(new RibbonFeature("K12.Retake.Shinmin.ClearingForm", "學期結算"));

            // 報表 及格人數
            Catalog catalog28 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog28.Add(new RibbonFeature("K12.Retake.Shinmin.Report.StudentPassReport", "及格人數"));

            // 報表 課程缺曠統計表
            Catalog catalog29 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalog29.Add(new RibbonFeature("K12.Retake.Shinmin.Report.CourseAttendanceRpt", "課程缺曠統計表"));

            // 報表 重補修缺課(含扣考)通知單
            Catalog catalogP01 = RoleAclSource.Instance["重補修"]["功能按鈕"];
            catalogP01.Add(new RibbonFeature("K12.Retake.Shinmin.Report.StudentCourseAttendanceRptForm", "重補修缺課(含扣考)通知單"));


            // 課程基本資料 資料項目
            FeatureAce UserPermission = FISCA.Permission.UserAcl.Current["K12.Retake.Shinmin.DetailContent.CourseInfoContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseInfoContent>());

            // 課程修課學生 資料項目
            UserPermission = FISCA.Permission.UserAcl.Current["K12.Retake.Shinmin.DetailContent.CourseStudentContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseStudentContent>());

            // 課程時間表 資料項目
            UserPermission = FISCA.Permission.UserAcl.Current["K12.Retake.Shinmin.DetailContent.CourseTimeSectionViewContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseTimeSectionViewContent>());



            // 資料項目權限註冊
            Catalog detailItem = RoleAclSource.Instance["重補修"]["資料項目"];
            detailItem.Add(new DetailItemFeature("K12.Retake.Shinmin.DetailContent.CourseInfoContent", "課程基本資料"));
            detailItem.Add(new DetailItemFeature("K12.Retake.Shinmin.DetailContent.CourseStudentContent", "課程修課學生"));
            detailItem.Add(new DetailItemFeature("K12.Retake.Shinmin.DetailContent.CourseTimeSectionViewContent", "課程時間表"));
        }

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("重補修 UDT Table 載入過程發生錯誤" + e.Error.Message);
            }
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            // 載入UDT table
            //// 成績輸入時間
            //UDTTransfer.UDTScoreInputDateSelect();
            //// 學生選課
            //UDTTransfer.UDTSsselectLoad();
            UDTTransfer.CreateRetakeUDTTable();
        }

        private static void DeleteCourse()
        {
            if (RetakeAdmin.Instance.SelectedSource.Count > 0)
            {
                DialogResult dr = MsgBox.Show("您確定要刪除課程?\n\n本功能將會串連刪除:\n(課程,修課學生,成績,缺曠)", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    AccessHelper _accessHelper = new AccessHelper();

                    //刪缺曠
                    List<UDTAttendanceDef> attendanceList = _accessHelper.Select<UDTAttendanceDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(attendanceList);

                    //時間區間
                    List<UDTTimeSectionDef> timeSectionList = _accessHelper.Select<UDTTimeSectionDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(timeSectionList);

                    //刪學生
                    List<UDTScselectDef> cselectList = _accessHelper.Select<UDTScselectDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(cselectList);

                    //刪課程
                    List<UDTCourseDef> courseList = _accessHelper.Select<UDTCourseDef>("UID in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(courseList);

                    MsgBox.Show("刪除成功!!");
                    RetakeEvents.RaiseAssnChanged();
                }
            }
            else
            {
                MsgBox.Show("請選擇課程!!");
            }
        }

    }
}
