using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Globalization;

partial class GUIClock : Form
{
	Timer timer;
	CultureInfo	culture_jp;
	GUIClock()
	{
		timer = new Timer();
		timer.Interval = 1000;
		timer.Tick += update_clock;
		timer.Start();
		culture_jp = new CultureInfo("ja-jp", false);
		culture_jp.DateTimeFormat.Calendar = new JapaneseCalendar();
		this.ClientSize = new Size(350, 200);
		this.Text = "詰め込み時計";
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.Load += (object sender, EventArgs e) => {
			update_clock(sender, e);
		};

		CreateControls();
		InitialControls();
	}

	public static void Main()
	{
		Application.Run(new GUIClock());
	}
	public DateTime GetTokyoTime()
	{
		TimeZoneInfo tokyoTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
		return TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo);
	}
}

partial class GUIClock
{
	Label lb_clock;
	Label lb_second;
	Label lb_am_pm;
	Label lb_ad;
	Label lb_era_name;
	Label lb_date;
	Label lb_day_of_week;
	FontFamily lb_font;
	Label lb_line;
	string[] day_of_week = {
		"日曜日",
		"月曜日",
		"火曜日",
		"水曜日",
		"木曜日",
		"金曜日",
		"土曜日",
	};

	void CreateControls()
	{
		lb_clock = new Label();
		lb_second = new Label();
		lb_am_pm = new Label();
		lb_line = new Label();
		lb_ad = new Label();
		lb_era_name = new Label();
		lb_date = new Label();
		lb_day_of_week = new Label();

		//Font
		var pfc = new PrivateFontCollection();
		pfc.AddFontFile(@"nitrods-font.ttf");
		lb_font = pfc.Families[0];
	}
	
	void InitialControls()
	{
		lb_clock.Text = "23:59_";
//		lb_clock.BackColor = Color.Orange;
		lb_clock.Font = new Font(lb_font, 48);
		lb_clock.Size = new Size(280, 80);
		lb_clock.Location = new Point(80, 10);

		lb_second.Text = "00";
//		lb_second.BackColor = Color.Pink;
		lb_second.Font = new Font(lb_font, 35);
		lb_second.Size = new Size(75, 60);
		lb_second.Location = new Point(265, 15);

		lb_am_pm.Text = "AM";
//		lb_am_pm.BackColor = Color.Orange;
		lb_am_pm.Font = new Font(lb_font, 48);
		lb_am_pm.Size = new Size(110, 80);
		lb_am_pm.Location = new Point(0, 10);

		lb_line.BackColor = Color.Black;
		lb_line.Size = new Size(310, 5);
		lb_line.Location = new Point(20, 90);

		lb_ad.Text = "2025";
//		lb_ad.BackColor = Color.Orange;
		lb_ad.Font = new Font(lb_font, 30);
		lb_ad.Size = new Size(120, 48);
		lb_ad.Location = new Point(0, 110);

		lb_era_name.Text = "令和7年";
//		lb_era_name.BackColor = Color.Orange;
		lb_era_name.Font = new Font(lb_font, 25);
		lb_era_name.Size = new Size(135, 48);
		lb_era_name.Location = new Point(0, 160);

		lb_date.Text = "12/31";
//		lb_date.BackColor = Color.Orange;
		lb_date.Font = new Font(lb_font, 48);
		lb_date.Size = new Size(235, 70);
		lb_date.Location = new Point(120, 90);

		lb_day_of_week.Text = "日曜日";
//		lb_day_of_week.BackColor = Color.Orange;
		lb_day_of_week.Font = new Font(lb_font, 25);
		lb_day_of_week.Size = new Size(48 * 3, 48);
		lb_day_of_week.Location = new Point(160, 160);

		Controls.Add(lb_line);
		Controls.Add(lb_second);
		Controls.Add(lb_clock);
		Controls.Add(lb_am_pm);
		Controls.Add(lb_ad);
		Controls.Add(lb_era_name);
		Controls.Add(lb_date);
		Controls.Add(lb_day_of_week);
	}

	void update_clock(object sender, EventArgs e)
	{
		DateTime time = GetTokyoTime();
		if(time.Hour > 12)
		{
			lb_clock.Text = (time.Hour - 12).ToString("D2") + ":" + time.Minute.ToString() + "＿";
		}
		else
		{
			lb_clock.Text = time.Hour.ToString("D2") + ":" + time.Minute.ToString() + "＿";
		}
		lb_second.Text		= time.ToString("ss");
		lb_am_pm.Text 		= time.Hour >= 12 ? "PM" : "AM";
		lb_ad.Text 			= time.Year.ToString();
		lb_era_name.Text	= time.ToString("ggy年", culture_jp);
		lb_date.Text 		= time.Month + "月" + time.Day + "日";
		lb_day_of_week.Text = day_of_week[(int)time.DayOfWeek];

		Invalidate();
	}
}





