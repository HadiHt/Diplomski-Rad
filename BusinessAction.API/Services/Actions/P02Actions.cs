using BusinessAction.API.IRepositories;
using BusinessAction.API.Models;
using Newtonsoft.Json;
using System.Text;

namespace BusinessAction.API.Services.Actions
{
    public class P02Actions
    {

        private readonly ILogger<P01Actions> _logger;
        private readonly HttpClient _httpClient;
        private readonly ICourseOfferingRepository _courseOfferingRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollementRepo _enrollementRepository;
        public string url = "http://localhost:8081/Camunda/api/addVariable";

        public P02Actions(ILogger<P01Actions> logger,
            HttpClient httpClient,
            ICourseOfferingRepository courseOfferingRepository,
            IMajorRepository majorRepository,
            IStudentRepository studentRepository,
            IEnrollementRepo enrollementRepository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _courseOfferingRepository = courseOfferingRepository;
            _majorRepository = majorRepository;
            _studentRepository = studentRepository;
            _enrollementRepository = enrollementRepository;
        }
        public async Task<GenericResponseModel> A02_01_CheckAvailability(CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing {nameof(A02_01_CheckAvailability)}");
            DataModel data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(request.data);
            var course = await _courseOfferingRepository.GetCourseOfferingsByCourseNameAsync(data.CourseName);

            CamundaControllerModel camundaControllerModel = new CamundaControllerModel();
            camundaControllerModel.processInstanceId = request.processInstanceId;
            camundaControllerModel.variableName = "Available";
            camundaControllerModel.variableType = "bool";

            if (course.SeatsAvailable != null && course.SeatsAvailable > 0)
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
            return new GenericResponseModel { Status = StatusCode.Error, Error = "No Seats Available" };
        }

        public async Task<GenericResponseModel> A02_02_AddCourse(CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing {nameof(A02_02_AddCourse)}");
            DataModel data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(request.data);
            CamundaControllerModel camundaControllerModel = new CamundaControllerModel();
            camundaControllerModel.processInstanceId = request.processInstanceId;
            camundaControllerModel.variableName = "IsSuccess";
            camundaControllerModel.variableType = "bool";
            bool isStudentEnrolled = false;
            var student = await _studentRepository.GetStudentByEmailAsync(data.Email);
            if (student != null)
            {
                var course = await _courseOfferingRepository.GetCourseOfferingsByCourseNameAsync(data.CourseName);
                isStudentEnrolled = await _enrollementRepository.EnrollStudentInCourseAsync(student, course.CourseID);
            }
            if (isStudentEnrolled)
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
            return new GenericResponseModel { Status = StatusCode.Error, Error = "No Seats Available inside course" };
        }
    }
}
