package TVZ.DiplomskiRad.Models;

public class ApplicationModel {
    private String woid;//
    private String Guid;//
    private String processDefinitionKey;//
    private String orderType;//
    private String fullName;
    private String email;
    private String dateOfBirth;
    private String jmbag;
    private String oib;
    private String major;
    private String course;

    public String getWoid() {
        return woid;
    }

    public void setWoid(String woid) {
        this.woid = woid;
    }

    public String getGuid() {
        return Guid;
    }

    public void setGuid(String guid) {
        Guid = guid;
    }

    public String getDateOfBirth() {
        return dateOfBirth;
    }

    public void setDateOfBirth(String dateOfBirth) {
        this.dateOfBirth = dateOfBirth;
    }

    public String getOrderType() {
        return orderType;
    }

    public void setOrderType(String orderType) {
        this.orderType = orderType;
    }

    public String getProcessDefinitionKey() {
        return processDefinitionKey;
    }

    public void setProcessDefinitionKey(String processDefinitionKey) {
        this.processDefinitionKey = processDefinitionKey;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getJmbag() {
        return jmbag;
    }

    public void setJmbag(String jmbag) {
        this.jmbag = jmbag;
    }

    public String getOib() {
        return oib;
    }

    public void setOib(String oib) {
        this.oib = oib;
    }

    public String getMajor() {
        return major;
    }

    public void setMajor(String major) {
        this.major = major;
    }

    public String getCourse() {
        return course;
    }

    public void setCourse(String course) {
        this.course = course;
    }
}
