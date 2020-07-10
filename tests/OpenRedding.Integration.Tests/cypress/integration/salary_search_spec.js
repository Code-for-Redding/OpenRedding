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
    
    it('should navigate to the salary search table on a successful response and clear the name and dropdown values when the user navigates back to the home page', () => {
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

        // Navigate back to the home page and assert the values are cleared
        cy.visit('/');
        cy.get('#salary-search-job-title').should('have.value', '');
        cy.get('#agency-dropdown').should('have.value', 'AllAgencies');
        cy.get('#status-dropdown').should('have.value', 'AllStatuses');
    });

    describe('search button', () => {
        // Setup and navigate to the search results page before each test
        beforeEach(() => {
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

            // Clear the engineer field
            cy.get('#salary-search-job-title').clear();
        });

        it('should enable the \'Search\' button for salaries when an valid value is entered for \'Name\'', () => {
            // Arrange, assert button is enabled
            const validName = 'Valid Name';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-name').type(validName);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        });
    
        it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Name\'', () => {
            // Arrange, assert button is enabled
            const invalidName = 'Inv@al#d N@m$';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-name').type(invalidName);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.disabled');
            cy.get('#salary-search-name').should('have.class', 'is-invalid');
        });
        
        it('should enable the \'Search\' button for salaries when an valid value is entered for \'Job Title\'', () => {
            // Arrange, assert button is enabled
            const validJobTitle = 'Valid Job Title';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-job-title').type(validJobTitle);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        });
    
        it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Job Title\'', () => {
            // Arrange, assert button is enabled
            const invalidJobTitle = 'Inv@al#d J0b t*tl3';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-job-title').type(invalidJobTitle);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.disabled');
            cy.get('#salary-search-job-title').should('have.class', 'is-invalid');
        });
    
        it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Job Title\' with a valid value for \'Name\'', () => {
            // Arrange, assert button is enabled
            const invalidJobTitle = 'Inv@al#d J0b t*tl3';
            const validName = 'Valid Name';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-job-title').type(invalidJobTitle);
            cy.get('#salary-search-name').type(validName);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.disabled');
            cy.get('#salary-search-job-title').should('have.class', 'is-invalid');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        });
    
        it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Name\' with a valid value for \'Job Title\'', () => {
            // Arrange, assert button is enabled
            const validJobTitle = 'Valid Job Title';
            const invalidName = '!@#123abc';
            cy.get('#refined-salary-search-button').should('be.enabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
            cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
            
            // Act
            cy.get('#salary-search-job-title').type(validJobTitle);
            cy.get('#salary-search-name').type(invalidName);
            
            // Assert
            cy.get('#refined-salary-search-button').should('be.disabled');
            cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
            cy.get('#salary-search-name').should('have.class', 'is-invalid');
        });
    });
});