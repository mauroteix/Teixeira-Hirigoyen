import { TestBed } from '@angular/core/testing';

import { MedicalconditionService } from './medicalcondition.service';

describe('MedicalconditionService', () => {
  let service: MedicalconditionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicalconditionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
