using Razor.Models;
using System.Text;
using System.Text.Json;

namespace Razor.Services
{
    public class MemberService : IMemberService
    {
        private const string apiEndpoint = "/Members/";
        private readonly IHttpClientFactory _httpClientFactory;
        private MemberViewModel _member;
        private IList<MemberViewModel> _members;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;


        public MemberService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};

            _client = _httpClientFactory.CreateClient("Member");
        }

        async Task<MemberViewModel> IMemberService.CreateMember(MemberViewModel member)
        {
            try
            {
                var serializedMember = JsonSerializer.Serialize(member);

                StringContent content = new StringContent(
                    serializedMember, Encoding.UTF8, "application/json");

                using (var response = await _client.PostAsync(apiEndpoint, content))
                {
                    if (!response.IsSuccessStatusCode) return null;

                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _member = string.IsNullOrEmpty(apiResponse.ToString()) ? null : await JsonSerializer.DeserializeAsync<MemberViewModel>(apiResponse, _options);
                }

                return _member;
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return null;
            }
            finally
            {
                /*Implementar Log*/
            }
        }

        async Task<bool> IMemberService.DeleteMember(string id)
        {
            try
            {
                using (var response = await _client.DeleteAsync(apiEndpoint + id))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return false;
            }
            finally
            {
                /*Implementar Log*/
            }
        }

        async Task<MemberViewModel> IMemberService.GetMember(string id)
        {
            try
            {
                using (var response = await _client.GetAsync(apiEndpoint + id))
                {
                    if (!response.IsSuccessStatusCode) return null;

                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _member = string.IsNullOrEmpty(apiResponse.ToString()) ? null : await JsonSerializer.DeserializeAsync<MemberViewModel>(apiResponse, _options);
                }

                return _member;
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return null;
            }
            finally
            {
                /*Implementar Log*/
            }
        }

        async Task<IList<MemberViewModel>> IMemberService.GetMembers()
        {
            try
            {
                using (var response = await _client.GetAsync(apiEndpoint))
                {
                    if (!response.IsSuccessStatusCode) return null;

                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _members = string.IsNullOrEmpty(apiResponse.ToString()) ? null : await JsonSerializer.DeserializeAsync<IList<MemberViewModel>>(apiResponse, _options);
                }

                return _members;
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return null;
            }
            finally
            {
                /*Implementar Log*/
            }
        }

        async Task<bool> IMemberService.UpdateMember(string id, MemberViewModel member)
        {
            try
            {
                using (var response = await _client.PutAsJsonAsync(apiEndpoint + id, member))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return false;
            }
            finally
            {
                /*Implementar Log*/
            }
        }
    }
}
