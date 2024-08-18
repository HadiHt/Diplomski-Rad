using BusinessAction.API.Controllers;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Models;
using BusinessAction.API.Models.DBModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static BusinessAction.API.Models.GenericResponseModel;

namespace BusinessAction.API.Services.Actions
{
    public class P01Actions
    {
        private readonly ILogger<P01Actions> _logger;
        private readonly HttpClient _httpClient;
        private readonly ICourseOfferingRepository _courseOfferingRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IStudentRepository _studentRepository;
        public string url = "http://localhost:8081/Camunda/api/addVariable";

        public P01Actions(ILogger<P01Actions> logger,
            HttpClient httpClient,
            ICourseOfferingRepository courseOfferingRepository,
            IMajorRepository majorRepository,
            IStudentRepository studentRepository) {
            _logger = logger;
            _httpClient = httpClient;
            _courseOfferingRepository = courseOfferingRepository;
            _majorRepository = majorRepository;
            _studentRepository = studentRepository;
        }
        public async Task<GenericResponseModel> A01_01_CheckAvailability(CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing {nameof(A01_01_CheckAvailability)}");
            DataModel data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(request.data);
            var major = await _majorRepository.GetMajorByCourseNameAsync(data.Major);

            CamundaControllerModel camundaControllerModel = new CamundaControllerModel();
            camundaControllerModel.processInstanceId = request.processInstanceId;
            camundaControllerModel.variableName = "Available";
            camundaControllerModel.variableType = "bool";

            if (major.AvailableSeats != null && major.AvailableSeats > 0)
            {
                camundaControllerModel.variableValue = "true";
                string jsonData = JsonConvert.SerializeObject(camundaControllerModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                return new GenericResponseModel { Status = StatusCode.Success, Error = null };
            }
            camundaControllerModel.variableValue = "false";
            string jsonData1 = JsonConvert.SerializeObject(camundaControllerModel);
            StringContent content1 = new StringContent(jsonData1, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = await _httpClient.PostAsync(url, content1);
            return new GenericResponseModel { Status = StatusCode.Error, Error = "No Seats Available in Major" };
        }
        public async Task<GenericResponseModel> A01_02_StartEnrollement(CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing {nameof(A01_02_StartEnrollement)}");
            DataModel data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(request.data);
            var major = await _majorRepository.GetMajorByCourseNameAsync(data.Major);
            major.AvailableSeats = major.AvailableSeats - 1;
            bool isSuccess = false;
            if (major != null)
            {
                Student newStudent = new Student();
                newStudent.FirstName = data.FullName.Split(" ")[0];
                newStudent.LastName = data.FullName.Split(" ")[1];
                newStudent.DateOfBirth = DateTime.Parse(data.DateOfBirth);
                newStudent.Email = data.Email;
                newStudent.Major = major;
                await _majorRepository.AddStudentToMajorAsync(major);
                isSuccess = await _studentRepository.AddStudentAsync(newStudent);
            }
            
            CamundaControllerModel camundaControllerModel = new CamundaControllerModel();
            camundaControllerModel.processInstanceId = request.processInstanceId;
            camundaControllerModel.variableName = "IsSuccess";
            camundaControllerModel.variableType = "bool";

            if (isSuccess)
            {
                camundaControllerModel.variableValue = "true";
                string jsonData = JsonConvert.SerializeObject(camundaControllerModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                return new GenericResponseModel { Status = StatusCode.Success, Error = null };
            }
            camundaControllerModel.variableValue = "false";
            string jsonData1 = JsonConvert.SerializeObject(camundaControllerModel);
            StringContent content1 = new StringContent(jsonData1, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = await _httpClient.PostAsync(url, content1);
            return new GenericResponseModel { Status = StatusCode.Error, Error = "failed to add Student" };
        }
    }
}
