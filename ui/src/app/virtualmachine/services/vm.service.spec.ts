import { TestBed } from '@angular/core/testing';

import { VMService } from './vm.service';

describe('VmService', () => {
  let service: VMService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VMService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
