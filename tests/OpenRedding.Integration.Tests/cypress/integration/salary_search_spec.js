describe('Salary search page', () => {
    beforeEach(() => cy.visit('/'));
    
    it('should navigate to the salary search table on a successful response', () => {
        // Arrange
        const jobTitle = 'engineer';
        const agency = 'Redding';
        const status = 'Full-time';

        // Act
        cy.get('#salary-search-job-title').type(jobTitle);
        cy.get('#agency-dropdown').select(agency);
        cy.get('#status-dropdown').select(status);
        cy.get('#salary-search-button').click();

        // Assert
        cy.contains('Enter values below, then hit the search button to refine your results');
        cy.get('#salary-search-job-title').should('have.value', jobTitle);
        cy.get('#agency-dropdown').should('have.value', 'Redding');
        cy.get('#status-dropdown').should('have.value', 'FullTime');
    });
});