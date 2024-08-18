package TVZ.DiplomskiRad.Models;

public class DataModel {
    private String CourseName;
    private String Major;
    private String FullName;
    private String DateOfBirth;
    private String Email;

    public String getFullName() {
        return FullName;
    }

    public void setFullName(String fullName) {
        FullName = fullName;
    }

    public String getDateOfBirth() {
        return DateOfBirth;
    }

    public void setDateOfBirth(String dateOfBirth) {
        DateOfBirth = dateOfBirth;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getMajor() {
        return this.Major;
    }

    public void setMajor(String major) {
        Major = major;
    }

    public String getCourseName() {
        return CourseName;
    }

    public void setCourseName(String CourseName) {
        this.CourseName = CourseName;
    }
}
