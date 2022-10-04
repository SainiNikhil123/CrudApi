import { TestBed } from '@angular/core/testing';

import { AuthAdminEmpGuardService } from './auth-admin-emp-guard.service';

describe('AuthAdminEmpGuardService', () => {
  let service: AuthAdminEmpGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthAdminEmpGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
