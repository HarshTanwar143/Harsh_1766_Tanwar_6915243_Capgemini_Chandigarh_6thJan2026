using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SmartHospital.MVC.ViewModels;

namespace SmartHospital.MVC.Services;

public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _baseUrl;

    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, IConfiguration config)
    {
        _httpClientFactory = factory;
        _httpContextAccessor = accessor;
        _baseUrl = config["ApiSettings:BaseUrl"] ?? "https://localhost:7000/api/";
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient("SmartHospitalAPI");
        client.BaseAddress = new Uri(_baseUrl);
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private StringContent Json(object obj) =>
        new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

    private async Task<T?> ReadAsync<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) return default;
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, _jsonOpts);
    }

    // Auth
    public async Task<(bool success, string? token, string? role, int userId, string? name, string? error)> LoginAsync(string email, string password)
    {
        var client = CreateClient();
        var response = await client.PostAsync("auth/login", Json(new { email, password }));
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            return (false, null, null, 0, null, "Invalid credentials");
        }
        var result = await ReadAsync<JsonElement>(response);
        var token = result.ValueKind != JsonValueKind.Undefined ? result.GetProperty("token").GetString() : null;
        var role = result.ValueKind != JsonValueKind.Undefined ? result.GetProperty("role").GetString() : null;
        var userId = result.ValueKind != JsonValueKind.Undefined ? result.GetProperty("userId").GetInt32() : 0;
        var name = result.ValueKind != JsonValueKind.Undefined ? result.GetProperty("fullName").GetString() : null;
        return (true, token, role, userId, name, null);
    }

    public async Task<(bool success, string? error)> RegisterAsync(string fullName, string email, string password)
    {
        var client = CreateClient();
        var response = await client.PostAsync("auth/register", Json(new { fullName, email, password, role = "Patient" }));
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            return (false, "Registration failed. Email may already be in use.");
        }
        return (true, null);
    }

    // Departments
    public async Task<List<DepartmentViewModel>> GetDepartmentsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("departments");
        return await ReadAsync<List<DepartmentViewModel>>(response) ?? new();
    }

    public async Task<DepartmentViewModel?> GetDepartmentAsync(int id)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"departments/{id}");
        return await ReadAsync<DepartmentViewModel>(response);
    }

    public async Task<bool> CreateDepartmentAsync(string name, string? description)
    {
        var client = CreateClient();
        var response = await client.PostAsync("departments", Json(new { departmentName = name, description }));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateDepartmentAsync(int id, string name, string? description)
    {
        var client = CreateClient();
        var response = await client.PutAsync($"departments/{id}", Json(new { departmentName = name, description }));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync($"departments/{id}");
        return response.IsSuccessStatusCode;
    }

    // Doctors
    public async Task<List<DoctorViewModel>> GetDoctorsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("doctors");
        return await ReadAsync<List<DoctorViewModel>>(response) ?? new();
    }

    public async Task<List<DoctorViewModel>> GetDoctorsByDepartmentAsync(int deptId)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"doctors/department/{deptId}");
        return await ReadAsync<List<DoctorViewModel>>(response) ?? new();
    }

    public async Task<DoctorViewModel?> GetDoctorAsync(int id)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"doctors/{id}");
        return await ReadAsync<DoctorViewModel>(response);
    }

    public async Task<bool> CreateDoctorAsync(CreateDoctorViewModel vm)
    {
        var client = CreateClient();
        var response = await client.PostAsync("doctors", Json(new
        {
            fullName = vm.FullName,
            email = vm.Email,
            password = vm.Password,
            departmentId = vm.DepartmentId,
            specialization = vm.Specialization,
            experienceYears = vm.ExperienceYears,
            availability = vm.Availability
        }));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateDoctorAsync(int id, UpdateDoctorPayload payload)
    {
        var client = CreateClient();
        var response = await client.PutAsync($"doctors/{id}", Json(payload));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync($"doctors/{id}");
        return response.IsSuccessStatusCode;
    }

    // Appointments
    public async Task<List<AppointmentViewModel>> GetMyAppointmentsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("appointments/my");
        return await ReadAsync<List<AppointmentViewModel>>(response) ?? new();
    }

    public async Task<List<AppointmentViewModel>> GetAllAppointmentsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("appointments");
        return await ReadAsync<List<AppointmentViewModel>>(response) ?? new();
    }

    public async Task<AppointmentViewModel?> GetAppointmentAsync(int id)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"appointments/{id}");
        return await ReadAsync<AppointmentViewModel>(response);
    }

    public async Task<bool> BookAppointmentAsync(int doctorId, DateTime date)
    {
        var client = CreateClient();
        var response = await client.PostAsync("appointments", Json(new { doctorId, appointmentDate = date }));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAppointmentStatusAsync(int id, string status)
    {
        var client = CreateClient();
        var response = await client.PutAsync($"appointments/{id}/status", Json(new { status }));
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelAppointmentAsync(int id)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync($"appointments/{id}");
        return response.IsSuccessStatusCode;
    }

    // Prescriptions
    public async Task<PrescriptionViewModel?> GetPrescriptionByAppointmentAsync(int appointmentId)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"prescriptions/appointment/{appointmentId}");
        return await ReadAsync<PrescriptionViewModel>(response);
    }

    public async Task<List<PrescriptionViewModel>> GetAllPrescriptionsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("prescriptions");
        return await ReadAsync<List<PrescriptionViewModel>>(response) ?? new();
    }

    public async Task<PrescriptionViewModel?> GetPrescriptionAsync(int id)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"prescriptions/{id}");
        return await ReadAsync<PrescriptionViewModel>(response);
    }

    public async Task<bool> CreatePrescriptionAsync(CreatePrescriptionViewModel vm)
    {
        var client = CreateClient();
        var response = await client.PostAsync("prescriptions", Json(new
        {
            appointmentId = vm.AppointmentId,
            diagnosis = vm.Diagnosis,
            medicines = vm.Medicines,
            notes = vm.Notes,
            medicineCharges = vm.MedicineCharges
        }));
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool success, decimal total)> ApprovePrescriptionAsync(int id, decimal medicineCharges)
    {
        var client = CreateClient();
        var response = await client.PostAsync($"prescriptions/{id}/approve", Json(new { prescriptionId = id, medicineCharges }));
        if (!response.IsSuccessStatusCode) return (false, 0);
        var result = await ReadAsync<JsonElement>(response);
        var total = result.ValueKind != JsonValueKind.Undefined ? result.GetProperty("totalAmount").GetDecimal() : 0;
        return (true, total);
    }

    // Bills
    public async Task<List<BillViewModel>> GetMyBillsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("bills/my");
        return await ReadAsync<List<BillViewModel>>(response) ?? new();
    }

    public async Task<List<BillViewModel>> GetAllBillsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("bills");
        return await ReadAsync<List<BillViewModel>>(response) ?? new();
    }

    public async Task<BillViewModel?> GetBillByAppointmentAsync(int appointmentId)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"bills/appointment/{appointmentId}");
        return await ReadAsync<BillViewModel>(response);
    }

    public async Task<bool> UpdatePaymentStatusAsync(int billId, string status)
    {
        var client = CreateClient();
        var response = await client.PutAsync($"bills/{billId}/payment", Json(new { paymentStatus = status }));
        return response.IsSuccessStatusCode;
    }

    // Users
    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("users");
        return await ReadAsync<List<UserViewModel>>(response) ?? new();
    }

    public async Task<List<UserViewModel>> GetPatientsAsync()
    {
        var client = CreateClient();
        var response = await client.GetAsync("users/patients");
        return await ReadAsync<List<UserViewModel>>(response) ?? new();
    }
}

public class UpdateDoctorPayload
{
    public int DepartmentId { get; set; }
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
}
