namespace KeyAutoPresser;

public partial class Form1 : Form
{
    private Keys? _targetKey;
    private System.Windows.Forms.Timer _timer;
    private bool _enabled = false;
    private bool _holding = false;

    // Win32 interop for global hotkey and SendInput
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    // MapVirtualKey for scancode
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    private const uint MAPVK_VK_TO_VSC = 0x0;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint KEYEVENTF_SCANCODE = 0x0008;

    // Optional: PostMessage fallback to the active window
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private const int WM_HOTKEY = 0x0312;
    private const int HOTKEY_ID = 1;
    private const uint WM_KEYDOWN = 0x0100;
    private const uint WM_KEYUP = 0x0101;

    public Form1()
    {
        InitializeComponent();
        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 100; // default
        _timer.Tick += (_, __) => DoSendKey();
        txtKey.Text = "Click here, then press a key";
        UpdateUi();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        // Register F8 with no modifiers
        RegisterHotKey(this.Handle, HOTKEY_ID, 0, (uint)Keys.F8);
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        UnregisterHotKey(this.Handle, HOTKEY_ID);
        base.OnHandleDestroyed(e);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY && m.WParam == (IntPtr)HOTKEY_ID)
        {
            Toggle();
            return;
        }
        base.WndProc(ref m);
    }

    private void Toggle()
    {
        if (_enabled)
        {
            _enabled = false;
            _timer.Stop();
            if (_holding)
            {
                ReleaseHeldKey();
                _holding = false;
            }
        }
        else
        {
            if (_targetKey == null)
            {
                MessageBox.Show("Pick a key first by clicking the box and pressing a key.", "No key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _enabled = true;
            _timer.Start();
        }
        UpdateUi();
    }

    private void UpdateUi()
    {
        btnToggle.Text = _enabled ? "Stop (or press F8)" : "Start (or press F8)";
        lblStatus.Text = _enabled ? "Status: Running" : "Status: Idle";
    }

    private void DoSendKey()
    {
        if (_targetKey == null) return;
        ushort vk = (ushort)_targetKey.Value;

        if (chkHold.Checked)
        {
            if (!_holding)
            {
                SendDown(vk);
                _holding = true;
            }
            return; // keep holding until stopped
        }

        // tap mode
        SendDown(vk);
        SendUp(vk);
    }

    private void ReleaseHeldKey()
    {
        if (_targetKey == null) return;
        SendUp((ushort)_targetKey.Value);
    }

    private void SendDown(ushort vk)
    {
        if (chkScanCode.Checked)
        {
            ushort scan = (ushort)MapVirtualKey(vk, MAPVK_VK_TO_VSC);
            var inputs = new INPUT[] { INPUT.ScanDown(scan) };
            var sent = SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
            if (sent == 0)
            {
                var hWnd = GetForegroundWindow();
                PostMessage(hWnd, WM_KEYDOWN, (IntPtr)vk, (IntPtr)(scan << 16));
            }
        }
        else
        {
            var inputs = new INPUT[] { INPUT.KeyDown(vk) };
            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
        }
    }

    private void SendUp(ushort vk)
    {
        if (chkScanCode.Checked)
        {
            ushort scan = (ushort)MapVirtualKey(vk, MAPVK_VK_TO_VSC);
            var inputs = new INPUT[] { INPUT.ScanUp(scan) };
            var sent = SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
            if (sent == 0)
            {
                var hWnd = GetForegroundWindow();
                PostMessage(hWnd, WM_KEYUP, (IntPtr)vk, (IntPtr)((scan << 16) | (1u << 30) | (1u << 31)));
            }
        }
        else
        {
            var inputs = new INPUT[] { INPUT.KeyUp(vk) };
            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
        }
    }

    // UI event handlers
    private void btnToggle_Click(object? sender, EventArgs e) => Toggle();

    private void numInterval_ValueChanged(object? sender, EventArgs e)
    {
        _timer.Interval = (int)numInterval.Value;
    }

    private void txtKey_KeyDown(object? sender, KeyEventArgs e)
    {
        // Ignore modifier-only keys
        if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
        {
            return;
        }
        _targetKey = e.KeyCode;
        txtKey.Text = _targetKey.ToString();
        e.SuppressKeyPress = true;
    }

    private void txtKey_MouseDown(object? sender, MouseEventArgs e)
    {
        // focus to capture key
        txtKey.Focus();
    }

    private void txtKey_Enter(object? sender, EventArgs e)
    {
        txtKey.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
    }

    private void txtKey_Leave(object? sender, EventArgs e)
    {
        txtKey.BackColor = System.Drawing.SystemColors.Window;
    }

    // Interop structures
    private struct INPUT
    {
        public uint type;
        public InputUnion u;

        public static INPUT KeyDown(ushort vk)
        {
            return new INPUT
            {
                type = 1, // INPUT_KEYBOARD
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = vk,
                        wScan = 0,
                        dwFlags = 0,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        public static INPUT KeyUp(ushort vk)
        {
            return new INPUT
            {
                type = 1, // INPUT_KEYBOARD
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = vk,
                        wScan = 0,
                        dwFlags = 0x0002, // KEYEVENTF_KEYUP
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        public static INPUT ScanDown(ushort scan)
        {
            return new INPUT
            {
                type = 1,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = scan,
                        dwFlags = KEYEVENTF_SCANCODE,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        public static INPUT ScanUp(ushort scan)
        {
            return new INPUT
            {
                type = 1,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = scan,
                        dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    private struct InputUnion
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        public MOUSEINPUT mi;
        [System.Runtime.InteropServices.FieldOffset(0)]
        public KEYBDINPUT ki;
        [System.Runtime.InteropServices.FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    private struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }
}
