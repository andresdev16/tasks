import { ChangeDetectorRef, Component } from '@angular/core';
import { MasterList } from '../../../../../core/model/mastar-list/master-list';
import { MasterCrudService } from '../../../../../core/services/master-crud/master-crud.service';
import { OverlayContainerService } from '../../../../../core/services/overlay-container/overlay-container.service';
import { environment } from '../../../../../../environments/environment';

const paths = environment.paths;

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss',
})
export class TaskListComponent extends MasterList {
  displayedColumns: string[] = [
    'check',
    'name',
    'category',
    'limitDate',
    'action',
  ];

  constructor(
    protected override crudService: MasterCrudService,
    protected override overlayContainerService: OverlayContainerService,
    protected override changeDetectorRef: ChangeDetectorRef
  ) {
    super(
      crudService,
      { uri: `${paths.task}`, uriComplement: '' },
      overlayContainerService,
      changeDetectorRef
    );
  }
}
