import axios from 'axios';

const API_URL = 'https://localhost:44312/api/';
class PublicService {
  getAllVehicleInsuranceContent() {
    return axios.get(API_URL + 'VehiclePolicy/non-logged');
  }
  getVehicleInsuranceContentById(id) {
    return axios.get(API_URL + `VehiclePolicy/${id}/non-logged`);
  }
}
export default new PublicService();