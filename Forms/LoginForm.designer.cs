﻿namespace BankApp.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_closeLoginForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txB_enterNumberPhone = new System.Windows.Forms.TextBox();
            this.txB_enterPassword = new System.Windows.Forms.TextBox();
            this.lbL_enterNumberPhone = new System.Windows.Forms.Label();
            this.lbL_enterPassword = new System.Windows.Forms.Label();
            this.chB_visibilityPassword = new System.Windows.Forms.CheckBox();
            this.btn_EnterLoginForm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbL_CreateAccount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_closeLoginForm
            // 
            this.btn_closeLoginForm.BackColor = System.Drawing.Color.GreenYellow;
            this.btn_closeLoginForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_closeLoginForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_closeLoginForm.ForeColor = System.Drawing.Color.Black;
            this.btn_closeLoginForm.Location = new System.Drawing.Point(368, 13);
            this.btn_closeLoginForm.Name = "btn_closeLoginForm";
            this.btn_closeLoginForm.Size = new System.Drawing.Size(24, 23);
            this.btn_closeLoginForm.TabIndex = 0;
            this.btn_closeLoginForm.Text = "X";
            this.btn_closeLoginForm.UseVisualStyleBackColor = false;
            this.btn_closeLoginForm.Click += new System.EventHandler(this.btn_closeLoginForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(178, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Вход";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Введите номер телефона";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(13, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Введите пароль";
            // 
            // txB_enterNumberPhone
            // 
            this.txB_enterNumberPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txB_enterNumberPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txB_enterNumberPhone.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_enterNumberPhone.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_enterNumberPhone.Location = new System.Drawing.Point(16, 92);
            this.txB_enterNumberPhone.Name = "txB_enterNumberPhone";
            this.txB_enterNumberPhone.Size = new System.Drawing.Size(373, 20);
            this.txB_enterNumberPhone.TabIndex = 4;
            // 
            // txB_enterPassword
            // 
            this.txB_enterPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txB_enterPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txB_enterPassword.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_enterPassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_enterPassword.Location = new System.Drawing.Point(15, 173);
            this.txB_enterPassword.Name = "txB_enterPassword";
            this.txB_enterPassword.Size = new System.Drawing.Size(377, 20);
            this.txB_enterPassword.TabIndex = 5;
            // 
            // lbL_enterNumberPhone
            // 
            this.lbL_enterNumberPhone.AutoSize = true;
            this.lbL_enterNumberPhone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_enterNumberPhone.ForeColor = System.Drawing.Color.GreenYellow;
            this.lbL_enterNumberPhone.Location = new System.Drawing.Point(13, 115);
            this.lbL_enterNumberPhone.Name = "lbL_enterNumberPhone";
            this.lbL_enterNumberPhone.Size = new System.Drawing.Size(379, 16);
            this.lbL_enterNumberPhone.TabIndex = 6;
            this.lbL_enterNumberPhone.Text = "................................................................................." +
    "............";
            // 
            // lbL_enterPassword
            // 
            this.lbL_enterPassword.AutoSize = true;
            this.lbL_enterPassword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_enterPassword.ForeColor = System.Drawing.Color.GreenYellow;
            this.lbL_enterPassword.Location = new System.Drawing.Point(12, 196);
            this.lbL_enterPassword.Name = "lbL_enterPassword";
            this.lbL_enterPassword.Size = new System.Drawing.Size(387, 16);
            this.lbL_enterPassword.TabIndex = 7;
            this.lbL_enterPassword.Text = "................................................................................." +
    "..............";
            // 
            // chB_visibilityPassword
            // 
            this.chB_visibilityPassword.AutoSize = true;
            this.chB_visibilityPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chB_visibilityPassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chB_visibilityPassword.Location = new System.Drawing.Point(280, 225);
            this.chB_visibilityPassword.Name = "chB_visibilityPassword";
            this.chB_visibilityPassword.Size = new System.Drawing.Size(112, 17);
            this.chB_visibilityPassword.TabIndex = 8;
            this.chB_visibilityPassword.Text = "показать пароль";
            this.chB_visibilityPassword.UseVisualStyleBackColor = true;
            this.chB_visibilityPassword.CheckedChanged += new System.EventHandler(this.chB_visibilityPassword_CheckedChanged);
            // 
            // btn_EnterLoginForm
            // 
            this.btn_EnterLoginForm.BackColor = System.Drawing.Color.GreenYellow;
            this.btn_EnterLoginForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_EnterLoginForm.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_EnterLoginForm.ForeColor = System.Drawing.Color.Black;
            this.btn_EnterLoginForm.Location = new System.Drawing.Point(12, 264);
            this.btn_EnterLoginForm.Name = "btn_EnterLoginForm";
            this.btn_EnterLoginForm.Size = new System.Drawing.Size(380, 38);
            this.btn_EnterLoginForm.TabIndex = 9;
            this.btn_EnterLoginForm.Text = "Войти";
            this.btn_EnterLoginForm.UseVisualStyleBackColor = false;
            this.btn_EnterLoginForm.Click += new System.EventHandler(this.btn_EnterLoginForm_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(81, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Нет учётной записи?";
            // 
            // lbL_CreateAccount
            // 
            this.lbL_CreateAccount.AutoSize = true;
            this.lbL_CreateAccount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_CreateAccount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbL_CreateAccount.Location = new System.Drawing.Point(216, 317);
            this.lbL_CreateAccount.Name = "lbL_CreateAccount";
            this.lbL_CreateAccount.Size = new System.Drawing.Size(104, 16);
            this.lbL_CreateAccount.TabIndex = 11;
            this.lbL_CreateAccount.Text = "Создать аккаунт";
            this.lbL_CreateAccount.Click += new System.EventHandler(this.lbL_CreateAccount_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.lbL_CreateAccount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn_EnterLoginForm);
            this.panel1.Controls.Add(this.chB_visibilityPassword);
            this.panel1.Controls.Add(this.lbL_enterPassword);
            this.panel1.Controls.Add(this.lbL_enterNumberPhone);
            this.panel1.Controls.Add(this.txB_enterPassword);
            this.panel1.Controls.Add(this.txB_enterNumberPhone);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_closeLoginForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 355);
            this.panel1.TabIndex = 12;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(404, 355);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "Вход";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_closeLoginForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txB_enterNumberPhone;
        private System.Windows.Forms.TextBox txB_enterPassword;
        private System.Windows.Forms.Label lbL_enterNumberPhone;
        private System.Windows.Forms.Label lbL_enterPassword;
        private System.Windows.Forms.CheckBox chB_visibilityPassword;
        private System.Windows.Forms.Button btn_EnterLoginForm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbL_CreateAccount;
        private System.Windows.Forms.Panel panel1;
    }
}

