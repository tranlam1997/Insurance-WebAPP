import axios from 'axios';

const API_URL = 'https://localhost:44312/api/';
class PublicService {
  getAllVehicleInsuranceContent() {
    return axios.get(API_URL + 'VehiclePolicy/non-logged');
  }
  getVehicleInsuranceContentById(id) {
    return axios.get(API_URL + `VehiclePolicy/${id}/non-logged`);
  }
  getAllMedicalInsuranceContent() {
    return axios.get(API_URL + 'MedicalPolicy/non-logged');
  }
  getMedicalInsuranceContentById(id) {
    return axios.get(API_URL + `MdeicalPolicy/${id}/non-logged`);
  }
  getAllLifeInsuranceContent() {
    return axios.get(API_URL + 'LifePolicy/non-logged');
  }
  getLifeInsuranceContentById(id) {
    return axios.get(API_URL + `LifePolicy/${id}/non-logged`);
  }
  getAllHomeInsuranceContent() {
    return axios.get(API_URL + 'HomePolicy/non-logged');
  }
  getHomeInsuranceContentById(id) {
    return axios.get(API_URL + `HomePolicy/${id}/non-logged`);
  }
  
}
export default new PublicService();